# PARTE 1: Questões teóricas.
## 1. Explique o que são os princípios SOLID e como cada um deles pode ser aplicado em um projeto desenvolvido em .NET.

**R:** SOLID é um acrônimo criado por Robert C. Martin (Uncle Bob). Cada letra se refere a um dos 5 principios, sendo eles:

- [Single Responsability Principle](SOLID-EXPLICACAO/QUESTOES-TEORICAS-SOLID-SRP.md)
- [Open Closed Principle](SOLID-EXPLICACAO/QUESTOES-TEORICAS-SOLID-OCP.md)
- [Liskov Substitution Principle](SOLID-EXPLICACAO/QUESTOES-TEORICAS-SOLID-LSP.md)
- [Interface Segregation Principle](SOLID-EXPLICACAO/QUESTOES-TEORICAS-SOLID-ISP.md)
- [Dependency Inversion Principle](SOLID-EXPLICACAO/QUESTOES-TEORICAS-SOLID-DIP.md)

## 2. Quais são os principais padrões de arquitetura de software utilizados em aplicações .NET? Descreva dois desses padrões e seus benefícios.
**R:** Essa pergunta é interessante, pois acredito que ela depende muito, pois cada projeto tem suas necessidades de arquitetura. Porém pensando por um lado mais simplista, eu acredito que duas se destacam na maioria dos projetos.

A primeira é a **Layered Architecture (Arquitetura em camadas):**

Como o nome já diz, com essa arquitetura, a gente divide o projeto em várias camadas. Sendo as principais: 

- **Camada de negócios:** Toda a regra de négocio da aplicação, onde vai ser processada as informações, a realização das validações e a interação com a camada de dados (Nossos famosos services).
- **Camada de dados:** Aqui ficam todas as operação de leitura e escrita nos bancos de dados. Após os dados serem processados na camada de negócios, eles são finalmente persistidos ou recuperados de um banco de dados (Nossos famosos repositories).
- **Camada de infraestrutura:** Nessa camada, ficam todos os componentes externos do projeto, como frameworks, serviços externos, e recursos da infra de um projeto. Um exemplo seria toda a configuração do Container IoC do .NET Core.

Os principais beneficios de usar a arquitetura em camadas, é que ela oferece diversas vantagens, alguns exemplos são a **"Separation of Concerns"** ou separação de preocupações, onde cada camada tem uma preocupação unica ou reponsabilidade unica (SRP entrando em prática aqui). Que facilita em muito a manutenção do sistema.

Outra vantagem é a **reutilização de código**. Pois cada camada desenvolvida, pode ser reutilizada em outras partes do projeto. Um exemplo é a camada de dados, onde cada repository criado, pode ser reutilizado em diversas partes.

**A segunda é a MVC (Model View e Controller):**

Uma coisa que não mencionei na arquitetura passada, é que a maioria dos padrões de arquiteturas de software, tem como base a Layered Architecture, pois sempre dividimos os projetos em camadas. Porém, eu vejo a MVC como mais centrada no negócio da aplicação, e não no software como o todo.

A MVC é divida em 3 principais camadas, sendo elas:

- **Model:** Representa os dados e regras de negócio da aplicação. Um exemplo seria nossa model NotaFiscal, ela representa todos os dados em relação a notas fiscais do nosso software, onde também aplicamos diversas regras de negócio.
- **View:** Aqui está toda a camada de apresentação do usuário, podemos ver essa camada como o html enviado ao usuario assim que acessa a pagina, ou o JSON enviado ao client-side.
- **Controller:** O controller é a camada responsável por interagir tanto com as models, quanto com a view. Um exemplo é que quando acessamos uma pagina, o responsavel por forneçar aquela pagina para nós, foi o controller. A mesma coisa quando fazemos uma requisição ao back-end para fornecer uma nota fiscal por exemplo, a primeira interação nessa requisição, é feita no controller.


Eu acredito que o principal beneficio da MVC é que com ela, podemos focar no negócio da aplicação, pois diferente da Layered, onde focamos nas camadas de um software por inteiro, aqui focamos nas camadas que devemos entregar para o cliente. Como as models, que podem ser vistas como clientes,e as apresentações, que é a própria interface do usuário. Mas também temos outros beneficios, como Manutenbilidade, Reutilização e testabilidade.


## 3. Por que é importante separar a lógica de negócios da lógica de apresentação em uma aplicação .NET? Como isso pode ser alcançado?
**R:** Separar a lógica de negócio da logica de apresentação, para mim é importante por vários motivos.

- **Manutenbilidade:** Separar essas camadas é importante, pois permite que cada camada não tenho um alto aclopamento, isso quer dizer que, caso alguma lógica seja alterada na lógica de negócio, isso não vai impactar fortemente a camada de apresentação, e vice-versa.
- **SRP:** Falando novamente sobre SOLID, eu acredito que o principío da responsabilidade unica, nos ajuda demais como desenvolvedor, pois além de evitar bugs, nos permite criar um código mais limpo e legivel. Separar essas camadas, vai nos fornecer esses beneficios, pois cada camada, teria sua unica responsabilidade.
- **Reusabilidade:** Separar cada camada, nos permite reaproveitar código e desta forma, reutilizar os componentes em diversas outras partes do projeto.
- **Escalabilidade:** Separando as camadas, permite que equipes diferente de um projeto, consigam trabalhar na mesma aplicação, cada um cuidando de uma responsabilidade.