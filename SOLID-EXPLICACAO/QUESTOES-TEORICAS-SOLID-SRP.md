# SINGLE RESPONSABILTY PRINCIPLE (SRP):
 
**Esse princípio nos diz: "Um módulo deve ser responsável por um e apenas um ator".**

Esse é o meu princípio favorito, pois na minha opinião, ele ajuda muito no desenvolvimento de classes, evitando o surgimento de "God Class (Classes que possui diversas responsabilidades)".

Vou utilizar um exemplo prático, para ficar claro.

**Exemplo:** Imagine que estamos desenvolvendo uma aplicação .NET e vamos precisar de um serviço de emissão de notas fiscais.
 
Para isso, vamos criar a classe **NotaFiscalService**. Nesta classe, temos as seguintes funções: **GerarNotaFiscal**, **FormatarNotaFiscal**, e **EnviarNotaFiscalPorEmail**.

```
namespace ProjetoP4P.services;

public class NotaFiscalService
{
    
    public NotaFiscal GerarNotaFiscal(string destinatario)
    {
        return new NotaFiscal()
        {
            Destinatario = destinatario
        };
    }

    public void FormatarNotaFiscal(NotaFiscal notaFiscal)
    {
        Console.WriteLine($"Formatando nota fiscal de código: {notaFiscal.Codigo}");
    }

    public void EnviarNotaFiscalPorEmail(string de, string para, string assunto, NotaFiscal nota)
    {
        Console.WriteLine($"Origem: {de}");
        Console.WriteLine($"Destinatario: {para}");
        Console.WriteLine($"Assunto: {assunto}");
        Console.WriteLine($"Nota Fiscal {nota}");
    }
}
```

Agora um truque que me ajuda no desenvolvimento de classes. Se eu trocar a palavra função por **responsabilidade**, podemos identificar a quebra do principio em nossa classe, pois nossa classe é responsavel por: Formatar uma nota fiscal, gerar uma nota e enviar-la para um email.

Sendo assim, nossa classe não possui apenas uma **responsabilidade ou "ator"**. É neste momento que podemos usar o principio **SRP** para nos ajudar.

Para iniciar, vamos criar 3 classes, sendo elas (sou péssimo com nome ok):
NotaFiscalGenerator, NotaFiscalFormater e EmailSender.

- **NotaFiscalGenerator** é responsavel apenas por gerar notas fiscais (Recebendo o método "GerarNotaFiscal")
```
namespace ProjetoP4P.services;

public class NotaFiscalGenerator
{
    public NotaFiscal GerarNotaFiscal(string nomeDestinatario)
    {
        return new NotaFiscal()
        {
            Destinatario = nomeDestinatario,
            Codigo = new Random().Next(0, 100)
        };
        
    }

}
```

- **NotaFiscalFormater** é responsavel por formatar nossa nota fiscal e organizar as informações necessárias (Recebendo o metodo "FormatarNotaFiscal").
```
namespace ProjetoP4P.services;

public class NotaFiscalFormater
{
    public void FormatarNotaFiscal(NotaFiscal notaFiscal)
    {
        Console.WriteLine($"Formatando nota fiscal de código: {notaFiscal.Codigo}");
    }
}
```
- **EmailSender** é responsavel por enviar e-mails.
**Obs:** Note que usei um nome genérico na classe EmailSender, pois desta forma, podemos reaproveita-la em outros contextos também. Seu método **EnviarNotaFiscalPorEmail** também seguiu essa abordagem, agora se chamando **"EnviarEmail"**.
```
namespace ProjetoP4P.services;

public class EmailSender
{
    public void EnviarEmail(string de, string para, string assunto, Object conteudo)
    {
        Console.WriteLine($"Origem: {de}");
        Console.WriteLine($"Destinatario: {para}");
        Console.WriteLine($"Assunto: {assunto}");
        Console.WriteLine($"Conteudo {conteudo}");

    }
}
```

Resultando em uma classe service mais limpa, e seguindo o principio SRP. 
```
namespace ProjetoP4P.services;

public class NotaFiscalService
{

    private readonly NotaFiscalGenerator _notaFiscalGenerator;
    private readonly NotaFiscalFormater _notaFiscalFormater;
    private readonly EmailSender _emailSender;

    public NotaFiscalService()
    {
        _notaFiscalGenerator = new NotaFiscalGenerator();
        _notaFiscalFormater = new NotaFiscalFormater();
        _emailSender = new EmailSender();
    }

    public void EnviarNotaFiscal(string destinatario, string email)
    {
        NotaFiscal notaFiscal = _notaFiscalGenerator.GerarNotaFiscal(destinatario);
        _notaFiscalFormater.FormatarNotaFiscal(notaFiscal);
        _emailSender.EnviarEmail("p4p@com.br", email, "Envio de nota fiscal", notaFiscal);
    }
    
}
```

Agora se você se perguntar, qual é a responsabilidade da NotaFiscalService? Enviar uma nota fiscal.
[Voltar para a pagina inicial](../QUESTOES-TEORICAS.md)
