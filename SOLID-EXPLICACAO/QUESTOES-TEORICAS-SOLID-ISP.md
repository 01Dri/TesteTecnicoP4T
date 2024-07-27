# INTERFACE SEGREGATION PRINCIPLE (ISP):
 
**Esse princípio nos diz: "Um cliente não deve ser forçado implementar uma interface que não vai usar".**

Esse principío é bem fácil de entender (bem mais que o LSP).

De forma direta, esse principío quer dizer que, uma classe  não deve implementar uma interface, com métodos que não vai utilizar.

Novamente com o exemplo de notas fiscais. Vamos criar uma interface chamada **INotaFiscal**. Nela vamos ter os métodos padrões de uma nota fiscal.

```
namespace ISP.Interfaces;

public interface INotaFiscal
{
    string Destinatario { get; set; }
    string ICMS { get; set; }
    string ObterDestinatario();
    string ObterICMS();
}
```

Agora, vamos criar uma classe que vai ser nossa nota fiscal CT-e.
```
using ISP.Interfaces;

namespace ISP.Models;

public class NotaFiscalCTE : INotaFiscal 
{
    public string Destinatario { get; set; }
    public string ICMS { get; set; }
    public string ObterDestinatario()
    {
        return this.Destinatario;
    }

    public string ObterICMS()
    {
        return this.ICMS;
    }
}
```
Tudo certo por aqui, porém, vamos criar agora uma classe MDF-e. E ela, no nosso contexto, não tem o atributo **ICMS**. Porém precisamos que ela receba o nosso método ObterDestinatario, que fica na interface INotaFiscal. (A interface INotaFiscal poderia ter diversos outros métodos).

Então vamos precisar implementar nossa interface.
```
using ISP.Interfaces;

namespace ISP.Models;

public class NotaFiscalMDF : INotaFiscal
{
    public string Destinatario { get; set; }
    public string ICMS { get; set; }
    public string ObterDestinatario()
    {
        return this.Destinatario;
    }

    public string ObterICMS()
    {
        return this.ICMS;
    }
}
```

Seguindo para nosso arquivo main, vamos utilizar o método ObterDestinatario e ObterICMS da classe CT-e. Porém, na nossa classe MDF-e, não queremos utilizar o método ObterICMS, já que no nosso contexto, esse tipo de nota fiscal, não precisa ter esse atributo.

Porém, mesmo sem precisarmos deste método, por padrão, toda classe que implementar INotaFiscal, necessita ter uma implementação do ICMS.

E é aqui que ferimos o principío, porque como ele diz "Um cliente não deve ser forçado implementar uma interface que não vai usar".

```
using ISP.Interfaces;
using ISP.Models;

INotaFiscal cte = new NotaFiscalCTE();
INotaFiscal mdf = new NotaFiscalMDF();


cte.ObterDestinatario();
cte.ObterICMS();

mdf.ObterDestinatario();
mdf.ObterICMS(); // Não precisamos utilizar este método, porém ele existe na nossa classe, pois ela é forçada a implementa-lo.

```

A forma de resolver este problema, e seguimos com o principío, é bem fácil na verdade. Para cada tipo de nota fiscal que não segue o padrão de INotaFiscal, devemos criar uma nova interface, com os novos comportamentos desta classe. 

No nosso exemplo, vamos criar uma interface INotaFiscalSemICMS, e remover os atributos e métodos relacionados a ICMS.

```
namespace ISP.Interfaces;

public interface INotaFiscalSemICMS
{
    string Destinatario { get; set; }
    string ObterDestinatario();
   
}
```

Agora, vamos voltar na nossa classe NotaFiscalMDF, e fazer ela implementar a interface INotaFiscalSemICMS, ao invés da INotaFiscal.

```
using ISP.Interfaces;

namespace ISP.Models;

public class NotaFiscalMDF : INotaFiscalSemICMS
{
    public string Destinatario { get; set; }
    public string ObterDestinatario()
    {
        return this.Destinatario;
    }

}
```

E está pronto, agora nossa classe NotaFiscalMDF não é forçada a implementar uma interface com métodos que não vai utilizar.
```
using ISP.Interfaces;
using ISP.Models;

INotaFiscal cte = new NotaFiscalCTE();
INotaFiscalSemICMS mdf = new NotaFiscalMDF();


cte.ObterDestinatario();
cte.ObterICMS();

mdf.ObterDestinatario();
mdf.ObterICMS(); // Não conseguimos chamar este método,  pois ela não existe nessa nota fiscal..

```

[Voltar para a pagina inicial](../QUESTOES-TEORICAS.md)

