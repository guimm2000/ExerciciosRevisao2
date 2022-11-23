using System;

public class Aluno
{
    public string Nome { get; private set; }
    public string Matricula { get; private set; }

    public float P1 { get; private set; }
    public float P2 { get; private set; }

    public Aluno(string nome, string matricula)
    {
        this.Nome = nome;
        this.Matricula = matricula;
        this.P1 = -1f;
        this.P2 = -1f;
    }

    public void LancarNotaP1(float nota)
    {
        if(P1 == -1f)
        {
            P1 = nota;
            return;
        }
        Console.WriteLine("Aluno ja possui nota da P1!");
    }
    public void LancarNotaP2(float nota)
    {
        if (P2 == -1f)
        {
            P2 = nota;
            return;
        }
        Console.WriteLine("Aluno ja possui nota da P2!");
    }

    public float NotaFinal()
    {
        return (P1 + P2) / 2;
    }
}

public class Turma
{
    List<Aluno> alunos;

    public Turma()
    {
        alunos = new List<Aluno>();
    }

    public void AddAluno(Aluno aluno)
    {
        if(alunos.Count == 0)
        {
            alunos.Add(aluno);
            Console.WriteLine("Aluno Matriculado na turma com sucesso.");
            return;
        }

        if(alunos.Contains(aluno))
        {
            Console.WriteLine("Aluno ja esta matriculado na turma!");
            return;
        }

        alunos.Add(aluno);
        Console.WriteLine("Aluno Matriculado na turma com sucesso.");
    }

    public void RemoveAluno(Aluno aluno)
    {
        for(int i = 0; i < alunos.Count; i++)
        {
            if (alunos[i].Equals(aluno))
            {
                alunos.Remove(aluno);
                Console.WriteLine("Aluno removido com sucesso.");
                return;
            }
        }
        Console.WriteLine("Aluno nao esta matriculado na turma!");
    }

    public void ImprimeAlunos()
    {
        var alunosOrdenados = alunos.OrderBy(a => a.Nome).ToList();
        foreach(Aluno aluno in alunosOrdenados)
        {
            Console.WriteLine("{0}\t Nota final: {1}", aluno.Nome , aluno.NotaFinal());
        }
    }

    public void ImprimeEstatisticas()
    {
        float mediaP1 = 0, mediaP2 = 0, mediaFinal = 0, maiorNotaFinal;

        maiorNotaFinal = alunos[0].NotaFinal();

        foreach(Aluno aluno in alunos)
        {
            mediaP1 += aluno.P1;
            mediaP2 += aluno.P2;
            mediaFinal += aluno.NotaFinal();

            if(aluno.NotaFinal() > maiorNotaFinal) maiorNotaFinal = aluno.NotaFinal();
        }
        mediaP1 /= alunos.Count();
        mediaP2 /= alunos.Count();
        mediaFinal /= alunos.Count();

        Console.WriteLine("Media da P1 = {0}\nMedia da P2 = {1}\nMedia da turma = {2}\n", mediaP1, mediaP2, mediaFinal);

        Console.WriteLine("Maior nota final = {0}\nAlunos que obtiveram maior nota final:\n", maiorNotaFinal);

        foreach(Aluno aluno in alunos)
        {
            if (aluno.NotaFinal() == maiorNotaFinal) Console.WriteLine("Nome: {0} \tMatricula: {1}\tNota da P1: {2}\tNota da P2: {3}", aluno.Nome, aluno.Matricula, aluno.P1, aluno.P2);
        }
    }
}

public class MainClass
{
    public static void Main(string[] args)
    {
        Aluno a = new Aluno("Joao da Silva", "1");
        a.LancarNotaP1(10);
        a.LancarNotaP2(10);

        Aluno b = new Aluno("Alfredo Santos", "2");
        b.LancarNotaP1(10);
        b.LancarNotaP2(5);

        Turma t = new Turma();
        t.AddAluno(a);
        t.AddAluno(b);

        Console.WriteLine();
        t.ImprimeAlunos();
        Console.WriteLine();
        t.ImprimeEstatisticas();
    }
}