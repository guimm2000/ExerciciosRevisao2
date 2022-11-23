using System;

public class Pessoa
{
    public string Nome { get; private set; }
    private CertidaoNascimento certidao = null;

    public Pessoa(string nome)
    {
        this.Nome = nome;
    }

    public Pessoa(string nome, CertidaoNascimento certidao)
    {
        this.Nome = nome;
        this.Certidao = certidao;
    }

    public CertidaoNascimento Certidao 
    { 
        get
        {
            return certidao;
        }
        private set
        {
            if(certidao == null) this.certidao = value;
        }
    }
    
    public void RegistrarCertidao(CertidaoNascimento certidao)
    {
        this.Certidao = certidao;
    }
}

public class CertidaoNascimento
{
    private readonly Pessoa pessoa;
    public DateTime DataEmissao { get; private set; }

    public CertidaoNascimento(Pessoa pessoa, DateTime dataEmissao)
    {
        this.DataEmissao = dataEmissao;
        this.pessoa = pessoa;
    }

    public Pessoa Pessoa
    {
        get
        {
            return pessoa;
        }
    }
}

public class MainClass
{
    public static void Main(string[] args)
    {

    }
} 