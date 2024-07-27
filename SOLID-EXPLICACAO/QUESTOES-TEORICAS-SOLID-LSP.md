# LISKOV SUBSTITUTION PRINCIPLE (LSP):
 
**Esse princípio nos diz: "Objetos de uma superclasse podem ser substituidos por objetos de sub classes sem afetar o funcionamento do programa".**

Esse principío para mim é o mais complicado. Mas vou tentar explicar de forma simples e prática.

Vou usar novamente o exemplo de notas fiscais, mas o contexto vai ser um pouco diferente dos anteriores.

Sabemos que notas fiscais tem vários tipos de documentos, alguns são: **NFS-e**, **CT-e** e **MDF-e**.

Mas todos eles no final, são **documentos** certo? Levando para o código, temos essa nossa super classe.

```
namespace ProjetoP4P.services;

public class DocumentoFiscal
{
    public string Destinatario { get; set; }
    public int Codigo { get; set;  }
    
    public string ICMS { get; set; }

    public virtual string ObterICMS()
    {
        return this.ICMS;
    }

    public string ObterDestinatario()
    {
        return this.Destinatario;
    }
    
}
```

Nela temos alguns atributos padrões como **Destinatário** e **Código**, mas também temos um atributo especifico chamado **ICMS**.

No nosso contexto, algumas notas não possuem **ICMS**, porém, todas as notas fiscais que herdarem nossa super classe, receberam esse método.

Temos as sub classes: 

```
namespace ProjetoP4P.services;

public class NotaFiscalCTE : DocumentoFiscal
{
    public override string ObterICMS()
    {
        return this.ICMS;
    }
    
}

namespace ProjetoP4P.services;

public class NotaFiscalMDF : DocumentoFiscal
{
    public override string ObterICMS()
    {
        return this.ICMS;
    }
}

namespace ProjetoP4P.services;

public class NotaFiscalNFS : DocumentoFiscal
{
    public override string ObterICMS()
    {
        throw new Exception("Notas NFS não possui chave de acesso.");
    }
}
```

Note que nossa sub classe **NotaFiscalNFS não possui ICMS**, então caso esse método seja chamado, ele irá lançar uma exceção.

No nosso main, vamos instanciar esses 3 tipos de notas e chamar o método **ObterICMS**

```
using ProjetoP4P.services;

DocumentoFiscal nfs = new NotaFiscalNFS();
DocumentoFiscal mdf = new NotaFiscalMDF();
DocumentoFiscal cte = new NotaFiscalCTE();

mdf.ObterICMS();
nfs.ObterICMS();
cte.ObterICMS();
```

Sim, esse código vai parar de rodar, assim que o nfs chamar o método ObterICMS, e é neste momento que ferimos o principio LSP, pois, nossas sub classes que herdam de DocumentoFiscal, **não estão substituindo sua super classe, e estão parando a execução de nosso programa**.

Para resolver este problema, e seguir com o principío **LSP**, vamos fazer algumas modificações.

Primeiro, vamos criar uma interface chamada **IDocumentoFiscal**, essa interface, vai definir atributos e métodos base para um Documento.

```
namespace ProjetoP4P.services;

public interface IDocumentoFiscal
{
    public string Destinatario { get; set; }
    public int Codigo { get; set;  }
    public string ObterDestinatario();
}
```

Agora, também precisamos criar uma classe abstrata chamada BaseDocumento, que vai implementar IDocumentoFiscal.

```
namespace ProjetoP4P.services;

public abstract class BaseDocumento : IDocumentoFiscal
{
    public string Destinatario { get; set; }
    public int Codigo { get; set; }
    public string ObterDestinatario()
    {
        return this.Destinatario;
    }
}
```
O próximo passo, é criar uma interface para definir atributos e métodos para um documento com ICMS. 

```
namespace ProjetoP4P.services;

public interface IDocumentFiscalICMS
{
    public string ICMS { get; set; }
    public string ObterICMS();
}
```

Agora vamos voltar para a classe DocumentoFiscal, e fazer ela herdar de BaseDocumento e IDocumentFiscalICMS.

```
namespace ProjetoP4P.services;

public class DocumentoFiscal : BaseDocumento, IDocumentFiscalICMS
{
    public string Destinatario { get; set; }
    public int Codigo { get; set; }
    public string ObterDestinatario()
    {
        return this.Destinatario;
    }

    public string ICMS { get; set; }
    public string ObterICMS()
    {
        throw new NotImplementedException();
    }
}
```

Dessa forma, nossas sub classes vão herdar DocumentoFiscal, e herdando DocumentoFiscal, ela também recebe a implementação de IDocumentoFiscalICMS.

Já a nossa sub classe NotaFiscalNFS, vai ser apenas um BaseDocumento, pois ela não necessita de ICMS.
```
namespace ProjetoP4P.services;

public class NotaFiscalNFS : BaseDocumento
{
}
```

Finalmente podemos voltar no main, e instanciar nossas sub classes, e assim, podemos reparar que a sub classe NotaFiscalNFS não tem o método ObterICMS, pois ela herda apenas BaseDocumento.

Porém, todas as classes no fim são BaseDocumento, sendo assim, todas as sub classes de BaseDocumento podem substituir sua super classe sem prejudicar o funcionamento do sistema.

```
using ProjetoP4P.services;

BaseDocumento nfs = new NotaFiscalNFS()
{
    Codigo = 1,
    Destinatario = "Diego"
};
DocumentoFiscal mdf = new NotaFiscalMDF()
{
    Codigo = 2,
    Destinatario = "Diego2",
    ICMS = "codigoICMS"
};
DocumentoFiscal cte = new NotaFiscalCTE()
{
    Codigo = 3,
    Destinatario = "Diego3",
    ICMS = "codigoICMS2"
};

Console.WriteLine(mdf.ObterICMS());
Console.WriteLine(cte.ObterICMS());
Console.WriteLine(nfs.ObterDestinatario());
```

[Voltar para a pagina inicial](../QUESTOES-TEORICAS.md)
