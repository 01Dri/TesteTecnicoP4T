# DEPENDENCY INVERSION PRINCIPLE (DIP):
 
**Esse princípio nos diz: "Módulos de alto nível não devem depender de módulos de baixo nível e ambos devem depender de abstrações.**

Eu vejo esse principío, como o mais utilizado, principalmente quando a gente fala de testes.

Como de costume, vou usar o exemplo de notas fiscais. Primeiro vamos criar uma classe que vai ser um repositório em memória para nossas notas fiscais.

```
using ProjetoP4P.services;

namespace DIP.Services;

public class NotaFiscalRepositoryMemoria
{
    private readonly IDictionary<int, NotaFiscal> data;

    public NotaFiscalRepositoryMemoria()
    {
        this.data = new Dictionary<int, NotaFiscal>();
    }

    public NotaFiscal? GetNotaFiscalPorCodigo(int codigo)
    {
        try
        {
            return this.data[codigo];

        }
        catch (Exception)
        {
            return null;
        }
    }

    public bool SalvarNotaFiscal(NotaFiscal notaFiscal)
    {
        try
        {
            this.data[notaFiscal.Codigo] = notaFiscal;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
```

Agora, vamos criar uma classe chamada **NotaFiscalService**, que vai ser uma classe que vai realizar as operações **CRUD** em nossa nota fiscal, e ela vai depender da implementação de **NotaFiscalRepositoryMemoria**.

```
using ProjetoP4P.services;

namespace DIP.Services;

public class NotaFiscalService
{
    private NotaFiscalRepositoryMemoria _fiscalRepository;

    public NotaFiscalService()
    {
        _fiscalRepository = new NotaFiscalRepositoryMemoria();
    }

    public bool SalvarNotaFiscal(NotaFiscal notaFiscal)
    {
        return this._fiscalRepository.SalvarNotaFiscal(notaFiscal);
    }

    public NotaFiscal GetNotaFiscalPorCodigo(int codigo)
    {
        NotaFiscal notaFiscal = this._fiscalRepository.GetNotaFiscalPorCodigo(codigo);
        if (notaFiscal == null)
        {
            throw new Exception("Nota fiscal não existe.");
        }

        return notaFiscal;
    }
}
```

Nesse código, já estamos ferindo o principío do **DIP**, pois nossa classe de alto nivel NotaFiscalService, está dependendo de uma implementação de NotaFiscalRepositoryMemoria. Porém, quero explicar um pouco melhor, quais são as consequências de ferir o principío.

O primeiro ponto, é que esse principio, nos ajuda demais em testes unitários. Isso porque, quando dependemos de abstrações, podemos criar mocks para essas classes. Um exemplo, vamos supor que nosso service, no método **SalvarNotaFiscal**, tenha uma verificação, se a nota fiscal é de um tipo especifico, pois caso ela seja, essa nota fiscal não deverá ser salva.

```
    public bool SalvarNotaFiscal(NotaFiscal notaFiscal)
    {
        if (notaFiscal.Tipo == TiposNotasFiscais.Cupom_Eletronico)
        {
            throw new Exception("Essa nota fiscal não deve ser salva.");
        }
        return this._fiscalRepository.SalvarNotaFiscal(notaFiscal);
    }
```
Como queremos fazer um teste unitário, não precisamos realmenter salvar uma nota fiscal, apenas verificar se as regras de negócios foram aplicadas e se ao menos o método de salvar foi ou não chamado, mas sem persistir de fato a nota. E para isso usariamos um Mock desta classe, que seria uma implementação "fictícia" do repositório, no qual não salvaria nada no banco.

Com a classe sendo uma abstração (interface) poderiamos fazer isso.

Um outro ponto é que depender de uma implementação, nos faz perder moduralidade. Isso pois, caso alterassemos alguma implementação da classe NotaFiscalRepositoryMemoria, teriamos que ajustar as modificações em todas as classes de alto nível que dependem dela.

Bom, para resolvermos estes problemas e seguir com o principío, primeiro vamos criar uma abstração para NotaFiscalRepositoryMemoria, que vamos chamar de IRepository.

```
using DIP.Models;

namespace DIP.Interfaces;

public interface IRepository
{
    public NotaFiscal? GetNotaFiscalPorCodigo(int codigo);
    public bool SalvarNotaFiscal(NotaFiscal notaFiscal);
}
```
Agora, vamos fazer nossa classe NotaFiscalRepositoryMemoria, implementar de IRepository

```
using DIP.Interfaces;
using ProjetoP4P.services;

namespace DIP.Services;

public class NotaFiscalRepositoryMemoria : IRepository
{
    private readonly IDictionary<int, Models.NotaFiscal> data;

    public NotaFiscalRepositoryMemoria()
    {
        this.data = new Dictionary<int, Models.NotaFiscal>();
    }

    public Models.NotaFiscal? GetNotaFiscalPorCodigo(int codigo)
    {
        try
        {
            return this.data[codigo];

        }
        catch (Exception)
        {
            return null;
        }
    }

    public bool SalvarNotaFiscal(Models.NotaFiscal notaFiscal)
    {
        try
        {
            this.data[notaFiscal.Codigo] = notaFiscal;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
```

O proximo passo, é fazer nossa classe NotaFiscalService, depender da abstração de IRepository.

```
using DIP.Interfaces;
using ProjetoP4P.services;
using NotaFiscal = DIP.Models.NotaFiscal;

namespace DIP.Services;

public class NotaFiscalService
{

    private readonly IRepository _repository;

    public NotaFiscalService(IRepository repository)
    {
        _repository = repository;
    }


    public bool SalvarNotaFiscal(NotaFiscal notaFiscal)
    {
        if (notaFiscal.Tipo == TiposNotasFiscais.Cupom_Eletronico)
        {
            throw new Exception("Essa nota fiscal não deve ser salva.");
        }
        return this._repository.SalvarNotaFiscal(notaFiscal);
    }

    public NotaFiscal GetNotaFiscalPorCodigo(int codigo)
    {
        NotaFiscal notaFiscal = this._repository.GetNotaFiscalPorCodigo(codigo);
        if (notaFiscal == null)
        {
            throw new Exception("Nota fiscal não existe.");
        }

        return notaFiscal;
    }
}
```

Perfeito, agora nossa classe de alto nível, depende de uma abstração e não uma implementação.

Agora vou um exemplo prático, sobre o que eu mencionei de modularidade. Mas para isso vou ter que usar um outro padrão **"Factory"** para implementar esse exemplo.

```
using DIP.Interfaces;
using DIP.Services;

namespace DIP.Factory;

public static class Factory
{
    public static IRepository CreateRepository()
    {
        return new NotaFiscalRepositoryMemoria();
    }
}
```

Esse exemplo, instancia uma implementação de IRepository, que vai ser utilizada no nosso service.

```
using DIP.Factory;
using DIP.Interfaces;
using DIP.Models;
using DIP.Services;

IRepository repository = Factory.CreateRepository();
NotaFiscalService service = new NotaFiscalService(repository);
service.SalvarNotaFiscal(new NotaFiscal()
{
    Codigo = 1,
    Destinatario = "Diego"
});

Console.WriteLine(service.GetNotaFiscalPorCodigo(1).Destinatario);

```

Agora, caso meu NotaFiscalService precise de uma nova implementação de um repository, um exemplo seria um repository que salva em um banco de dados de fato, poderia simplesmente alterar a forma de instanciação na Factory.

```
using DIP.Interfaces;
using DIP.Services;

namespace DIP.Factory;

public static class Factory
{
    public static IRepository CreateRepository()
    {
        return new NotaFiscalRepositoryPostgres();
    }
}
```


E dessa forma, não precisamos alterar nada no nosso service NotaFiscalService, pois ela apenas depende de uma abstração, que pode ser qualquer implementação definida na Factory.


[Voltar para a pagina inicial](../QUESTOES-TEORICAS.md)
 