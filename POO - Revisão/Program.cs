using System;

// Exceção personalizada para dados inválidos
public class ExcecaoDadoInvalido : Exception
{
    public ExcecaoDadoInvalido(string mensagem) : base(mensagem)
    {
    }
}

// Exceção personalizada para elemento já existente
public class ExcecaoElementoJaExistente : Exception
{
    public ExcecaoElementoJaExistente(string mensagem) : base(mensagem)
    {
    }
}

// Exceção personalizada para elemento inexistente
public class ExcecaoElementoInexistente : Exception
{
    public ExcecaoElementoInexistente(string mensagem) : base(mensagem)
    {
    }
}

// Exceção personalizada para repositório cheio
public class ExcecaoRepositorio : Exception
{
    public ExcecaoRepositorio(string mensagem) : base(mensagem)
    {
    }
}

// Classe de Negócio: Conta
public class Conta
{
    public int Numero { get; set; }

    // Método set com tratamento de exceção para dado inválido
    public void SetNumero(int numero)
    {
        if (numero <= 0)
        {
            throw new ExcecaoDadoInvalido("Número de conta inválido.");
        }
        Numero = numero;
    }
}

// Classe de Negócio: CadastroContas
public class CadastroContas
{
    private Conta[] contas;
    private int indice;

    // Construtor que inicializa o array de contas
    public CadastroContas(int capacidade)
    {
        contas = new Conta[capacidade];
        indice = 0;
    }

    // Método inserir com tratamento de exceção para repositório cheio e elemento já existente
    public void Inserir(Conta novaConta)
    {
        if (indice >= contas.Length)
        {
            throw new ExcecaoRepositorio("Não é possível mais inserir contas no array.");
        }

        // Verifica se a conta já existe antes de adicionar
        if (ContaJaExistente(novaConta.Numero))
        {
            throw new ExcecaoElementoJaExistente("Conta com mesmo número já cadastrada.");
        }

        contas[indice] = novaConta;
        indice++;
    }

    // Método buscar com tratamento de exceção para elemento inexistente
    public Conta Buscar(int numeroConta)
    {
        foreach (var conta in contas)
        {
            if (conta != null && conta.Numero == numeroConta)
            {
                return conta;
            }
        }

        throw new ExcecaoElementoInexistente("Conta não encontrada.");
    }

    // Método auxiliar para verificar se a conta já existe
    private bool ContaJaExistente(int numeroConta)
    {
        foreach (var contaExistente in contas)
        {
            if (contaExistente != null && contaExistente.Numero == numeroConta)
            {
                return true;
            }
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        try
        {
            // Exemplo de uso

            CadastroContas cadastro = new CadastroContas(3);

            Conta conta1 = new Conta();
            conta1.SetNumero(1);

            Conta conta2 = new Conta();
            conta2.SetNumero(2);

            Conta conta3 = new Conta();
            conta3.SetNumero(3);

            cadastro.Inserir(conta1);
            cadastro.Inserir(conta2);
            cadastro.Inserir(conta3);

            // Tenta inserir uma conta repetida (lançará ExcecaoElementoJaExistente)
            Conta contaRepetida = new Conta();
            contaRepetida.SetNumero(2);
            cadastro.Inserir(contaRepetida);

            // Busca uma conta que não existe (lançará ExcecaoElementoInexistente)
            Conta contaInexistente = cadastro.Buscar(4);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
