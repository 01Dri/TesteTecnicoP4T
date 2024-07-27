# OPEN CLOSED PRINCIPLE (OCP):
 
**Esse princípio nos diz "Uma classe deve ser aberta para extensões e fechada para modificações"**.

Tá, mas o que isso quer dizer ? Novamente vou usar um exemplo prático para explicar.

Voltando no nosso exemplo de notas fiscais. Agora nosso serviço de gerar notas, deve gerar uma nota baseada em seu tipo, com isso podemos fazer a seguinte modificação: 
```
namespace ProjetoP4P.services;

public class NotaFiscalGenerator
{
    public NotaFiscal GerarNotaFiscal(string nomeDestinatario, int tipo)
    {
        NotaFiscal notaFiscal = new NotaFiscal()
        {
            Destinatario = nomeDestinatario,
            Codigo = new Random().Next(0, 100)
        };
        
        if (tipo == 1)
        {
            notaFiscal.Tipo = TiposNotasFiscais.Venda_Produto;
        }
        
        if (tipo == 2)
        {
            notaFiscal.Tipo = TiposNotasFiscais.Servico;
        }
        if (tipo == 3)
        {
            notaFiscal.Tipo = TiposNotasFiscais.Cupom_Eletronico;
        }

        return notaFiscal;
    }

}
```
A gente tem alguns problemas com essa abordagem.

O principal é que sempre que um novo tipo de nota surgir, vai ser necessário criar uma nova condicional para o gerador, e isso a longo prazo, vai gerar uma quantidade de código gigantesca. E uma coisa que aprendi é, **"Quanto mais código, mais bug".**

Também podemos reparar que esta classe fere o principio de **OCP**, pois a cada novo tipo, uma nova modificação deve ser feita na função, ferindo complemente o principio.

Para seguir o principio, vai ser necessário algumas modificações. A primeira é que vamos criar uma interface genérica para gerar notas fiscais, com o método **"GerarNotaFiscal".**

```
namespace ProjetoP4P.services.Interfaces;

public interface INotaFiscalGenerator
{
    public NotaFiscal GerarNotaFiscal(string nomeDestinatario);
}
```

Agora, para cada tipo de nota fiscal, vamos criar uma classe que implementa essa interface, e gerar uma nota baseada no seu tipo. Dessa forma, vamos estar sempre estendendo esse serviço e não modificando.

Um exemplo do tipo 1.
```
using ProjetoP4P.services.Interfaces;

namespace ProjetoP4P.services;

public class NotaFiscalGeneratorVendaProduto : INotaFiscalGenerator
{
    public NotaFiscal GerarNotaFiscal(string nomeDestinatario)
    {
        return new NotaFiscal()
        {
            Destinatario = nomeDestinatario,
            Codigo = new Random().Next(0, 100),
            Tipo = TiposNotasFiscais.Venda_Produto
        };
        
    }
}
```

**Obs:** Eu utilizei o padrão "Factory" e o "Service Locator" para dado um determinado tipo, instanciar a classe generator corretamente.

O resultado é este:
```
using ProjetoP4P.services.Interfaces;

namespace ProjetoP4P.services;

public class NotaFiscalFactory
{
    private readonly Dictionary<int, Func<INotaFiscalGenerator>> _funcs =
        new Dictionary<int, Func<INotaFiscalGenerator>>();

    public NotaFiscalFactory()
    {
        _funcs[1] = () => new NotaFiscalGeneratorVendaProduto();
        _funcs[2] = () => new NotaFiscalGeneratorServico();
        _funcs[3] = () => new NotaFiscalGeneratorCupomEletronico();
    }

    public void RegistrarNotaFiscalGenerator(int tipo, Func<INotaFiscalGenerator> generator)
    {
        _funcs[tipo] = generator;
    }

    public  INotaFiscalGenerator BuildNotaFiscalGenerator(int tipo)
    {
        return _funcs[tipo]();
    }
}
```

Dessa forma, para cada novo tipo de nota, podemos registrar um novo tipo de implementação de INotaFiscalGenerator (Estendendo e não modificando)

O resultado final do **NotaFiscalGenerator** é este:

```
namespace ProjetoP4P.services;

public class NotaFiscalGenerator
{
    private readonly NotaFiscalFactory _notaFiscalFactory;

    public NotaFiscalGenerator()
    {
        _notaFiscalFactory = new NotaFiscalFactory();
    }

    public NotaFiscal GerarNotaFiscal(string nomeDestinatario, int tipo)
    {
        return _notaFiscalFactory.BuildNotaFiscalGenerator(tipo).GerarNotaFiscal(nomeDestinatario);
    }

}
```

[Voltar para a pagina inicial](../QUESTOES-TEORICAS.md)