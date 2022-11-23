using System;

public class Propriedades
{
    string path;
    public Propriedades() { }
    public Propriedades(string path) 
    { 
        if(!File.Exists(path))
        {
            throw new FileNotFoundException("Arquivo nao existe!");
        }

       this.path = path;
    }

    public string ValorChave(string chave)
    {
        string[] lines = File.ReadAllLines(path);

        for(int i = 0; i < lines.Length; i++) 
        {
            if (lines[i].Contains(chave))
            {
                return lines[i].Remove(chave.Length+1);
            }
        }
        return null;
    }

    public void AlterarChave(string chave, string valor)
    {
        bool existe = false;
        string[] lines = File.ReadAllLines(path);
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains(chave))
            {
                lines[i].Remove(chave.Length+1, lines[i].Length+chave.Length+1);
                lines[i] += valor;
                existe = true;
            }
        }
        if(!existe)
        {
            lines.Append(chave + "=" +valor);
        }
        FileStream f = File.Create(path);
        File.WriteAllLines(path, lines);
        f.Close();
    }

    public bool ChaveExiste(string chave)
    {
        chave = chave + "=";

        return File.ReadAllText(path).Contains(chave);
    }
}