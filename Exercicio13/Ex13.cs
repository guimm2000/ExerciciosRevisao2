using System;

public abstract class Progressao
{
    protected int Primeiro { get; private set; }
    protected int Razao { get; private set; }

    //Foi a unica forma que consegui resolver o proximo valor
    protected int suporte;

    protected readonly int proximoValor;
    public abstract int ProximoValor { get; }
    
    public Progressao(int primeiro, int razao)
    {
        this.Primeiro = primeiro;
        this.Razao = razao;
    }

    public abstract void Reinicializar();
    public abstract int TermoAt(int posicao);
}

public class ProgressaoAritmetica : Progressao
{

    public ProgressaoAritmetica(int primeiro, int razao):base(primeiro, razao)
    {
        suporte = 0;
    }

    public override int ProximoValor
    {
        get 
        {
            if (suporte == 0) suporte = Primeiro;
            suporte += Razao;
            return suporte;
        }
    }

    public override void Reinicializar()
    {
        suporte = 0;
    }
    public override int TermoAt(int posicao)
    {
        return Primeiro+((posicao-1) * Razao);
    }

}

public class ProgressaoGeometrica : Progressao
{

    public ProgressaoGeometrica(int primeiro, int razao) : base(primeiro, razao)
    {
        suporte = 0;
    }
    public override int ProximoValor
    {
        get 
        {
            if (suporte == 0) suporte = Primeiro;
            suporte *= Razao;
            return suporte;
        }
    }
    public override void Reinicializar()
    {
        suporte = 0;
    }
    public override int TermoAt(int posicao)
    {
        return Primeiro*Convert.ToInt32(Math.Pow(Convert.ToDouble(Razao), Convert.ToDouble(posicao-1)));
    }

}

public class MainClass
{
    public static void Main(string[] args)
    {
        ProgressaoAritmetica pa = new ProgressaoAritmetica(3, 4);
        ProgressaoGeometrica pg = new ProgressaoGeometrica(3, 4);

        Console.WriteLine("PA:");
        for(int i = 1; i <= 10; i++)
        {
           Console.WriteLine(pa.TermoAt(i));
        }

        Console.WriteLine("PG:");
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine(pg.TermoAt(i));
        }
    }
} 