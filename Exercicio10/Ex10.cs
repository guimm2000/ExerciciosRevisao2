using System;

public class Aluno
{
    public string Nome { get; private set; }
    public string Matricula { get; private set; }
    public Turma Turma { get; private set; }

    private static int matriculaAtual = 0;

    public Aluno(string nome)
    {
        this.Nome = nome;
        matriculaAtual++;
        this.Matricula = matriculaAtual.ToString();
        this.Turma = null;
    }

    public void SeMatricular(Turma turma)
    {
        if (Turma.Equals(null))
        {
            this.Turma = turma;
            return;
        }
        Console.WriteLine("Aluno ja matriculado em uma turma!");
    }

    public void AddTurma(Turma t)
    {
        if (Turma.Equals(null))
        {
            this.Turma = t;
            return;
        }
    }

    public void RemoverTurma()
    {
        this.Turma = null;
    }
}

public class Turma
{
    public string Codigo { get; private set; }
    private static List<string> codigos = new List<string>();
    public List<Aluno> Alunos { get; private set; }

    public Turma(string codigo)
    {
        if(codigos.Count() > 0 && codigos.Contains(codigo))
        {
            throw new CodigoTurmaExcecao("Este codigo de turma ja existe!");
        }
        this.Codigo = codigo;
        codigos.Add(codigo);
    }

    public void AddAluno(Aluno a)
    {
        if(a.Turma.Equals(null) && !this.Alunos.Contains(a))
        {
            this.Alunos.Add(a);
            a.AddTurma(this);
            Console.WriteLine("Aluno adicionado na turma!");
            return;
        }
        Console.WriteLine("Impossivel adicionar aluno na turma!");
    }

    public void RemoveAluno(Aluno a)
    {
        if(Alunos.Contains(a))
        {
            this.Alunos.Remove(a);
            a.RemoverTurma();
            Console.WriteLine("Aluno removido da turma!");
            return;
        }
        Console.WriteLine("Erro ao remover aluno da turma!");
    }

    public void ListarAlunos()
    {
        var alunosOrdenados = this.Alunos.OrderBy(a => a.Nome).ToList();
        foreach (Aluno aluno in alunosOrdenados)
        {
            Console.WriteLine("-{0}", aluno.Nome);
        }
    }
}

public class Curso
{
    public string Nome { get; private set; }
    public List<Aluno> Alunos { get; private set; }
    public List<Turma> Turmas { get; private set; }

    public Curso(string nome)
    {
        this.Nome = nome;
        this.Alunos = new List<Aluno>();
        this.Turmas = new List<Turma>();
    }

    public void CriarTurma(string codigo)
    {
        try
        {
            Turma t = new Turma(codigo);
            this.Turmas.Add(t);
            Console.WriteLine("Turma adicionada no curso!");
        }
        catch(CodigoTurmaExcecao e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void MatricularAluno(Aluno a)
    {
        if(Alunos.Count() == 0)
        {
            Alunos.Add(a);
            Console.WriteLine("Aluno matriculado no curso!");
            return;
        }
        else
        {
            if(Alunos.Contains(a))
            {
                Console.WriteLine("Aluno ja esta matriculado no curso!");
                return;
            }
        }
        Alunos.Add(a);
        Console.WriteLine("Aluno matriculado no curso!");
    }

    public void RemoverAluno(Aluno a)
    {
        if(a.Turma.Equals(null))
        {
            this.Alunos.Remove(a);
            Console.WriteLine("Aluno removido do curso!");
            return;
        }
        Console.WriteLine("Impossivel remover aluno do curso!");
    }

    public void RemoverTurma(Turma t)
    {
        if(t.Alunos.Count() == 0)
        {
            this.Turmas.Remove(t);
            Console.WriteLine("Turma removida do curso!");
            return;
        }
        Console.WriteLine("Turma deve estar vazia para ser removida do curso!");
    }

    public void ListarTurmas()
    {
        var TurmasOrdenadas = this.Turmas.OrderBy(t => t.Codigo).ToList();
        foreach (Turma t in TurmasOrdenadas)
        {
            if(t.Alunos.Count() > 0)
            {
                Console.WriteLine("Turma: {0}", t.Codigo);
                t.ListarAlunos();
                Console.WriteLine();
            }
        }
    }

}

public class CodigoTurmaExcecao : Exception
{
    public CodigoTurmaExcecao(string message)
        :base(message)
    {
    }
}

public class MainClass
{
    public static void Main(string[] args)
    {

    }
}