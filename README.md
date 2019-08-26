# Usando a interface IComparable no C#

## Introdução

Muitas vezes é necessário criar coleções de objetos complexos e os comparar e classificar de alguma forma, mas isto nem sempre é uma tarefa fácil, ainda mais quando não temos um ID sequencial, ou qualquer outra informação que possa trazer do banco a ordenação correta.

Comparar objetos complexos também é um desafio, pois não estamos lidando com tipos primitivos como int e string que retornam apenas um valor puro numérico ou texto. Tipos complexos podem conter várias informações relevantes que podem ou não fazer sentido para classificações e comparações.

A interface IComparable permite uma implementação padrão para que, juntamente com os recursos da namespace System.Linq, possa ser possível ordenar e comparar dados.

## Classe Dog

Vamos criar uma lista de cachorros com o nome da raça de cada um, comparar e listar em ordem alfabética. A classe Dog  possui um ID do tipo Guid.

## Crie o a solução e os projetos

1. Crie uma pasta C:\GitHub\CSharpIComparable,
2. Abra a pasta no Visual Studio Code,
3. Acesse via terminal,
4. Para criar a solução digite o comando a seguir:

```Powershell
dotnet new sln
```

5. Para criar o projeto de domínio, digite o comando a seguir:

```Powershell
dotnet new classlib --output ./src/CSharpIComparable.Domain
```

6. Para criar o projeto de console, digite o comando a seguir:

```Powershell
dotnet new console --output ./src/CSharpIComparable.Prompt
```

7. Inclua os dois projetos na solução

```Powershell
dotnet sln add .\src\CSharpIComparable.Domain\
```

```Powershell
dotnet sln add .\src\CSharpIComparable.Prompt\
```

8. O projeto de console precisa fazer referência o projeto de domínio, para isso execute o comando a seguir.

```Powershell
dotnet add .\src\CSharpIComparable.Prompt\  reference .\src\CSharpIComparable.Domain\
```

Agora temos uma estrutura para trabalhar.

## Crie a classe Dog no projeto CSharpIComparable.Domain

A classe Dog possuiu dois campos, um com ID do tipo Guid e um nome para definir a raça.

```CSharp
using System;

namespace CSharpIComparable.Domain
{
    public class Dog
    {
        public Dog(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
```

## Altere a classe Program do projeto CSharpIComparable.Prompt

Crie a lista de Dogs e exiba em tela com um foreach. A coleção será mostrada na ordem que foi preenchida.

```CSharp
using System;
using System.Collections.Generic;
using CSharpIComparable.Domain;

namespace CSharpIComparable.Prompt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Dog Colection
            var dogs = new List<Dog>()
            {
                new Dog(Guid.NewGuid(), "Doberman"),
                new Dog(Guid.NewGuid(), "Poodle"),
                new Dog(Guid.NewGuid(), "Bulldog"),
                new Dog(Guid.NewGuid(), "Sheltie"),
                new Dog(Guid.NewGuid(), "Beagle")                
            };

            // Show in the prompt
            foreach (var dog in dogs)
            {
                Console.WriteLine(dog.Name);
            }
        }
    }
}
```

Digite o comando a seguir para exibir a lista:

```Powershell
dotnet run --project .\src\CSharpIComparable.Prompt\
```

E veja o resultado:

```Powershell
PS C:\GitHub\CSharpIComparable> dotnet run --project .\src\CSharpIComparable.Prompt\
Doberman
Poodle
Bulldog
Sheltie
Beagle
PS C:\GitHub\CSharpIComparable>
```

A lista exibida está ordenada exatamente como cadastrei, sem nenhuma alteração.

## Ordene os dados

Agora que temos uma coleção, podemos ordenar os dados alfabeticamente. Para isso, a namespace System.Linq possui a extensão **.Sort()**, mas se tentarmos usá-la agora, vamos receber uma mensagem de erro.

>Unhandled Exception: System.InvalidOperationException: Failed to compare two elements in the array. ---> System.ArgumentException: At least one object must implement IComparable.

O erro diz que ocorreu uma falha ao comparar dois elementos e que pelo menos um deve implementar IComparable. E por isso vamos alterar a classe Dog para seja possível fazer esta comparação e ordenar os dados corretamente:

1. Implemente a classe IComparable:

```CSharp
public class Dog : IComparable
```

2. Altere o método CompareTo para que seja comparado com o campo nome:

```CSharp
public int CompareTo(object obj)
{
    return Name.CompareTo((obj as Dog).Name);
}
```

3. Sobrescreva o método ToString() para facilir a leitura:

```CSharp
public override string ToString() => Name;
```

4. Confira classe Dog completa.

```CSharp
using System;

namespace CSharpIComparable.Domain
{
    public class Dog : IComparable
    {
        public Dog(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public int CompareTo(object obj)
        {
            return Name.CompareTo((obj as Dog).Name);
        }

        public override string ToString() => Name;
    }
}
```

5. Altere a classe Program.

```CSharp
using System;
using System.Collections.Generic;
using CSharpIComparable.Domain;

namespace CSharpIComparable.Prompt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Dog Colection
            var dogs = new List<Dog>()
            {
                new Dog(Guid.NewGuid(), "Doberman"),
                new Dog(Guid.NewGuid(), "Poodle"),
                new Dog(Guid.NewGuid(), "Bulldog"),
                new Dog(Guid.NewGuid(), "Sheltie"),
                new Dog(Guid.NewGuid(), "Beagle")                
            };

            // Show in the prompt
            Console.WriteLine("Unordered Dog list\n");
            foreach (var dog in dogs)
            {
                Console.WriteLine(dog.Name);
            }

            // Sort the list
            dogs.Sort();

            // Show sorted list in the prompt
            Console.WriteLine("\nOrdered Dog list\n");
            dogs.ForEach(Console.WriteLine);
        }
    }
}
```

6. Excute e veja a lista ordenada:

```Powershell
PS C:\GitHub\CSharpIComparable> dotnet run --project .\src\CSharpIComparable.Prompt\
Unordered Dog list

Doberman
Poodle
Bulldog
Sheltie
Beagle

Ordered Dog list

Beagle
Bulldog
Doberman
Poodle
Sheltie
PS C:\GitHub\CSharpIComparable>
```

## Comparando objetos

Além de ajudar na ordenação, agora podemos comparar dois objetos e verificar a similaridade entre eles. Para isso as alterações que fizemos na classe Dog já nos permite isso.

1. Na classe Program, vamos instanciar quatro objetos dogs, compará-los e analisar sua precedência em relação ao objeto original:

```CSharp
using System;
using System.Collections.Generic;
using CSharpIComparable.Domain;

namespace CSharpIComparable.Prompt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Dog Colection
            var dogs = new List<Dog>()
            {
                new Dog(Guid.NewGuid(), "Doberman"),
                new Dog(Guid.NewGuid(), "Poodle"),
                new Dog(Guid.NewGuid(), "Bulldog"),
                new Dog(Guid.NewGuid(), "Sheltie"),
                new Dog(Guid.NewGuid(), "Beagle")                
            };

            // Show in the prompt
            Console.WriteLine("Unordered Dog list\n");
            foreach (var dog in dogs)
            {
                Console.WriteLine(dog.Name);
            }

            // Sort the list
            dogs.Sort();

            // Show sorted list in the prompt
            Console.WriteLine("\nOrdered Dog list\n");
            dogs.ForEach(Console.WriteLine);

            // Comparing objects
            var dog1 = new Dog(Guid.NewGuid(), "Doberman");
            var dog2 = new Dog(Guid.NewGuid(), "Bulldog");
            var dog3 = new Dog(Guid.NewGuid(), "Beagle");
            var dog4 = new Dog(Guid.NewGuid(), "Doberman");

            Console.WriteLine("\nComparing objects\n");
            Console.WriteLine($"{dog1}.CompareTo({dog2}) : {(dog1.CompareTo(dog2))}" );
            Console.WriteLine($"{dog2}.CompareTo({dog3}) : {(dog2.CompareTo(dog3))}" );
            Console.WriteLine($"{dog1}.CompareTo({dog3}) : {(dog1.CompareTo(dog3))}" );
            Console.WriteLine($"{dog3}.CompareTo({dog1}) : {(dog3.CompareTo(dog1))}" );   
            Console.WriteLine($"{dog1}.CompareTo({dog4}) : {(dog1.CompareTo(dog4))}" );
        }
    }
}
```

2. Execute o projeto e veja o resultado

```Powershell
PS C:\GitHub\CSharpIComparable> dotnet run --project .\src\CSharpIComparable.Prompt\
Unordered Dog list

Doberman
Poodle
Bulldog
Sheltie
Beagle

Ordered Dog list

Beagle
Bulldog
Doberman
Poodle
Sheltie

Comparing objects

Doberman.CompareTo(Bulldog) : 1
Bulldog.CompareTo(Beagle) : 1
Beagle.CompareTo(Doberman) : -1
Doberman.CompareTo(Doberman) : 0
PS C:\GitHub\CSharpIComparable>
```

3. Entendo os resultados:

Na primeira ao comparar Doberman com Bulldog e retorna 1, significa que Doberman está a frente de Bulldog na ordem alfabética, a mesma coisa acontece quando comparamos Bulldog com Beagle.

Na terceira, quando comparamos Beagle com Doberman, é retornado -1, ou seja, Beagle vem antes de Doberman.

Na última, quando quando comparamos dois objetos iguais, Doberman com Doberman, o resultado é 0, ou seja, a posição ordinal é a mesma.

## Concluindo

Há outras formas de ordenar usando a System.Linq, mas extensão **.Sort()** facilita esta empreitada e deixa o código mais limpo e claro.

## Algumas referências

- C# IComparable no canal Jamie King: https://www.youtube.com/watch?v=-Qxuvv5fXSg
- IComparable Interface: https://docs.microsoft.com/en-us/dotnet/api/system.icomparable?view=netframework-4.8
- IComparable.CompareTo(Object) Method: https://docs.microsoft.com/en-us/dotnet/api/system.icomparable.compareto?view=netframework-4.8