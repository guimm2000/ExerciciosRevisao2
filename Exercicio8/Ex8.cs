using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Cliente
{
    public string Nome { get; private set; }
    public long CPF { get; private set; }
    public DateTime DataDeNascimento { get; private set; }
    public float RendaMensal { get; private set; }
    public char EstadoCivil { get; private set; }
    public int Dependentes { get; private set; }

    public Cliente(string nome, string cpf, string dataNascimento, string rendaMensal, string estadoCivil, string dependentes)
    {
        ValidacaoCliente validacao = new ValidacaoCliente(ref nome, ref cpf, ref dataNascimento, ref rendaMensal, ref estadoCivil, ref dependentes);
        Nome = nome;
        CPF = long.Parse(cpf);
        DataDeNascimento = DateTime.ParseExact(dataNascimento, "dd/MM/yyyy", null);
        //Trocando a virgula por ponto para o funcionamento da variavel float de forma adequada
        RendaMensal = float.Parse(Regex.Replace(rendaMensal, ",", "."));
        EstadoCivil = char.Parse(estadoCivil);
        Dependentes = int.Parse(dependentes);
    }
}

public class ValidacaoCliente
{
    public bool valido;

    List<string> pendencias = new List<string>();

    public ValidacaoCliente() 
    { 
        this.valido = false;
        pendencias.Add("nome");
        pendencias.Add("cpf");
        pendencias.Add("data");
        pendencias.Add("idade");
        pendencias.Add("renda");
        pendencias.Add("estadocivil");
        pendencias.Add("dependentes");

    }

    public ValidacaoCliente(ref string nome, ref string cpf, ref string dataNascimento, ref string rendaMensal, ref string estadoCivil, ref string dependentes) : this()
    {

        while(!valido)
        {
            Regex regexData = new Regex(@"^\d{2}\/\d{2}\/\d{4}");
            Regex regexRenda = new Regex(@"^\d+\,{1}\d{2}");
            if (nome.Length > 5)
            {
                pendencias.Remove("nome");
            }

            if(ValidaCpf(cpf)) pendencias.Remove("cpf");

            if (regexData.IsMatch(dataNascimento))
            {
                pendencias.Remove("data");
            }

            //Checando a idade
            DateTime datatemp = DateTime.ParseExact(dataNascimento, "dd/MM/yyyy", null);
            int idade = DateTime.Now.Year - datatemp.Year;

            if (DateTime.Now.Month > datatemp.Month)
            {
                idade--;
            }
            else
            {
                if (DateTime.Now.Month == datatemp.Month)
                {
                    if (DateTime.Now.Day > datatemp.Day)
                    {
                        idade--;
                    }
                }
            }

            if (idade > 18)
            {
                pendencias.Remove("idade");
            }

            if (regexRenda.IsMatch(rendaMensal)) { 

                pendencias.Remove("renda");
            }

            if (estadoCivil.Equals("C") || estadoCivil.Equals("S") || !estadoCivil.Equals("V") || !estadoCivil.Equals("D"))
            {
                pendencias.Remove("estadocivil");
            }

            if (int.Parse(dependentes) <= 10 && int.Parse(dependentes) >= 0)
            {
                pendencias.Remove("dependentes");
            }

        }
    }
    private bool ValidaCpf(string cpf)
    {
        if(cpf.Length < 11) return false;

        bool igual = true;
        for(int i = 1; i < cpf.Length; i++)
        {
            if (cpf[i] != cpf[i-1])
            {
                igual = false;
            }
        }
        if(igual) return false;

        int j, k, soma = 0;

        for(int i = 0; i < 9; i++)
        {
            soma += (cpf[i] - '0')*(10-i);
        }

        if (soma % 11 == 0 || soma % 11 == 1) j = 0;
        else j = 11 - (soma % 11);

        if (j != cpf[9] - '0') return false;

        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += (cpf[i] - '0') * (11 - i);
        }

        if (soma % 11 == 0 || soma % 11 == 1) k = 0;
        else k= 11 - (soma % 11);

        if (k != cpf[10] - '0') return false;

        return true;
    }
}