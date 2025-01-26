using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
class Candidato
{
    private string nomeCandidato;
    private double mediacandidato;
    private double notaRedacao;
    private double notaMat;
    private double notaLinguas;
    private int primeiraop;
    private int segundaop;

    public Candidato(string nomeCandidato, double mediacandidato, double notaRedacao, double notaMat, double notaLinguas, int primeiraop, int segundaop)
    {
        this.nomeCandidato = nomeCandidato;
        this.mediacandidato = mediacandidato;
        this.notaRedacao = notaRedacao;
        this.notaMat = notaMat;
        this.notaLinguas = notaLinguas;
        this.primeiraop = primeiraop;
        this.segundaop = segundaop;
    }

    public Candidato() { }

    public string NomeCandidato
    {
        get { return nomeCandidato; }
        set { nomeCandidato = value; }
    }

    public double MediaCandidato
    {
        get { return mediacandidato; }
        set { mediacandidato = value; }
    }

    public double NotaRedacao
    {
        get { return notaRedacao; }
        set { notaRedacao = value; }
    }

    public double NotaMat
    {
        get { return notaMat; }
        set { notaMat = value; }
    }

    public double NotaLinguas
    {
        get { return notaLinguas; }
        set { notaLinguas = value; }
    }

    public int PrimeiraOP
    {
        get { return primeiraop; }
        set { primeiraop = value; }
    }

    public int SegundaOP
    {
        get { return segundaop; }
        set { segundaop = value; }
    }

    public double Media(double notaRedacao, double notaMat, double notaLinguas)
    {
        double media = (notaRedacao + notaMat + notaLinguas) / 3;
        return media;
    }
}
class Curso
{
    private int codCurso;
    private string nomeCurso;
    private int qtdvagasdocurso;
    public List<Candidato> selecionados;
    public Fila filaEspera;
    public Curso(string nomeCurso, int qtdvagasdocurso)
    {
        this.nomeCurso = nomeCurso;
        this.qtdvagasdocurso = qtdvagasdocurso;
        selecionados = new List<Candidato>();
        filaEspera = new Fila(10);
    }
    public Curso() { }
    public int CodCurso
    {
        get { return codCurso; }
        set { codCurso = value; }
    }

    public string NomeCurso
    {
        get { return nomeCurso; }
        set { nomeCurso = value; }
    }

    public int QtdVagasDoCurso
    {
        get { return qtdvagasdocurso; }
        set { qtdvagasdocurso = value; }
    }

    public bool InserirCandidato(Candidato candidato)
    {
        if (selecionados.Count < qtdvagasdocurso)
        {
            selecionados.Add(candidato);
            return true;
        }
        return false;
    }
    public double CalcNotadcorte()
    {
        double menorNota = double.MaxValue;
        for (int i = 0; i < selecionados.Count; i++)
        {
            if (selecionados[i].MediaCandidato < menorNota)
            {
                menorNota = selecionados[i].MediaCandidato;
            }
        }
        return menorNota;
    }
}
class Vestibular
{
    public void quicksort(List<Candidato> selecionados, int esq, int dir)
    {
        int i = esq, j = dir;
        Candidato pivo = selecionados[(esq + dir) / 2];
        while (i <= j)
        {

            while (CompararNotas(selecionados[i], pivo) < 0)
            {
                i++;
            }
            while (CompararNotas(selecionados[j], pivo) > 0)
            {
                j--;
            }
            if (i <= j)
            {
                Trocar(selecionados, i, j);
                i++;
                j--;

            }
        }
        if (esq < j)
        {
            quicksort(selecionados, esq, j);
        }
        if (i < dir)
        {
            quicksort(selecionados, i, dir);

        }
    }
    void Trocar(List<Candidato> selecionados, int i, int j)
    {
        Candidato temp = selecionados[i];
        selecionados[i] = selecionados[j];
        selecionados[j] = temp;
    }
    private static int CompararNotas(Candidato a, Candidato b)
    {
        int resultado = b.MediaCandidato.CompareTo(a.MediaCandidato);
        if (resultado != 0)
        {
            return resultado;
        }
        resultado = b.NotaRedacao.CompareTo(a.NotaRedacao);
        if (resultado != 0)
        {
            return resultado;
        }
        resultado = b.NotaMat.CompareTo(a.NotaMat);
        if (resultado != 0)
        {
            return resultado;
        }
        return b.NotaLinguas.CompareTo(a.NotaLinguas);
    }
}
class Dicionario
{
    public void ImprimirDicionario(Dictionary<int, Curso> dicionario)
    {
        Console.WriteLine("----Dicionário------");
        foreach (KeyValuePair<int, Curso> item in dicionario)
        {
            Console.WriteLine($"Chave = {item.Key}, Nome = {item.Value.NomeCurso}, Quantidade = {item.Value.QtdVagasDoCurso}");
        }
    }
}

class Fila
{
    Candidato[] filaEspera;
    int primeiro, ultimo;

    public Fila(int tamanho)
    {
        filaEspera = new Candidato[tamanho + 1];
        primeiro = ultimo = 0;
    }
    public void Inserir(Candidato x)
    {
        if (((ultimo + 1) % filaEspera.Length) == primeiro)
        {
            throw new Exception("Erro!");
        }
        filaEspera[ultimo] = x;
        ultimo = (ultimo + 1) % filaEspera.Length;
    }
    public string Mostrar()
    {
        string resultado = "";
        int i = primeiro;
        while (i != ultimo)
        {
            resultado += $"{filaEspera[i].NomeCandidato} {Math.Round(filaEspera[i].MediaCandidato, 1)} {filaEspera[i].NotaRedacao} {filaEspera[i].NotaMat} {filaEspera[i].NotaLinguas}\n";
            i = (i + 1) % filaEspera.Length;
        }
        return resultado;
    }
}
class Lista
{
    public void ImprimirLista(List<Candidato> candidatos)
    {
        Console.WriteLine("----Lista de Candidatos----");
        foreach (Candidato c in candidatos)
        {
            Console.WriteLine($"Nome: {c.NomeCandidato}, Média: {Math.Round(c.MediaCandidato, 1)} Nota Redação: {c.NotaRedacao} Nota Matemática: {c.NotaMat} Nota Linguagens: {c.NotaLinguas} Primeira Opção: {c.PrimeiraOP}. Segunda Opção: {c.SegundaOP}");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Curso selecdocurso = new Curso();
        Lista imprimirLista = new Lista();
        List<Candidato> candidatos = new List<Candidato>();
        Dicionario imprimirDicionario = new Dicionario();
        Vestibular vestibular = new Vestibular();
        Candidato candidato = new Candidato();
        Dictionary<int, Curso> dicionario = new Dictionary<int, Curso>();
        int count = 0, qtdCursos = 0, qtdCandidatos = 0, codCurso = 0, qtdeVagasCursos = 0, primeiraOp, segundaOp;
        string linha, nomeCurso, nomeCandidato;
        double notaRedacao = 0, notaMat = 0, notaLinguagens = 0, mediaCandidato = 0;
        string[] tamanho = new string[2];
        string[] infocursos = new string[3];
        string[] infocandidato = new string[6];
        try
        {
            StreamReader arquivo = new StreamReader("entrada.txt", Encoding.UTF8);
            linha = arquivo.ReadLine();
            while (linha != null)
            {
                count++;
                //Console.WriteLine(linha);
                if (count == 1)
                {
                    tamanho = linha.Split(';');
                    qtdCursos = int.Parse(tamanho[0]);
                    qtdCandidatos = int.Parse(tamanho[1]);
                }
                if (count <= qtdCursos + 1 && count >= 2)
                {
                    infocursos = linha.Split(";");
                    codCurso = int.Parse(infocursos[0]);
                    nomeCurso = infocursos[1];
                    qtdeVagasCursos = int.Parse(infocursos[2]);
                    dicionario.Add(codCurso, new Curso(nomeCurso, qtdeVagasCursos));
                }
                if (count > qtdCursos + 1)
                {
                    infocandidato = linha.Split(";");
                    nomeCandidato = infocandidato[0];
                    notaRedacao = double.Parse(infocandidato[1]);
                    notaMat = double.Parse(infocandidato[2]);
                    notaLinguagens = double.Parse(infocandidato[3]);
                    primeiraOp = int.Parse(infocandidato[4]);
                    segundaOp = int.Parse(infocandidato[5]);
                    mediaCandidato = candidato.Media(notaRedacao, notaMat, notaLinguagens);
                    candidatos.Add(new Candidato(nomeCandidato, mediaCandidato, notaRedacao, notaMat, notaLinguagens, primeiraOp, segundaOp));
                }
                linha = arquivo.ReadLine();
            }
            //imprimirDicionario.ImprimirDicionario(dicionario);
            //imprimirLista.ImprimirLista(candidatos);
            arquivo.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        Console.WriteLine("Lista Ordenada");
        vestibular.quicksort(candidatos, 0, candidatos.Count - 1);
        foreach (Candidato c in candidatos)
        {
            Console.WriteLine($"Nome: {c.NomeCandidato}, Média: {Math.Round(c.MediaCandidato, 1)} Nota Redação: {c.NotaRedacao} Nota Matemática: {c.NotaMat} Nota Linguagens: {c.NotaLinguas} Primeira Opção: {c.PrimeiraOP}. Segunda Opção: {c.SegundaOP}");
        }
        foreach (Candidato cand in candidatos)
        {
            if (dicionario.ContainsKey(cand.PrimeiraOP))
            {
                bool inseridoNaPrimeiraOpcao = dicionario[cand.PrimeiraOP].InserirCandidato(cand);
                if (!inseridoNaPrimeiraOpcao && dicionario.ContainsKey(cand.SegundaOP))
                {
                    bool inseridoNaSegundaOpcao = dicionario[cand.SegundaOP].InserirCandidato(cand);

                    if (inseridoNaSegundaOpcao)
                    {
                        if (dicionario.ContainsKey(cand.PrimeiraOP))
                        {
                            dicionario[cand.PrimeiraOP].filaEspera.Inserir(cand);
                        }
                    }
                    else
                    {
                        if (dicionario.ContainsKey(cand.PrimeiraOP))
                        {
                            dicionario[cand.PrimeiraOP].filaEspera.Inserir(cand);
                        }
                        if (dicionario.ContainsKey(cand.SegundaOP))
                        {
                            dicionario[cand.SegundaOP].filaEspera.Inserir(cand);
                        }
                    }
                }
            }
        }
        using (StreamWriter arqsaida = new StreamWriter("saida.txt"))
        {
            foreach (KeyValuePair<int, Curso> selec in dicionario)
            {
                int codigo = selec.Key;
                Curso curso = selec.Value;
                double notadcorte = Math.Round(curso.CalcNotadcorte(), 1);
                arqsaida.WriteLine($"{curso.NomeCurso} {notadcorte}");
                arqsaida.WriteLine("Selecionados:");
                foreach (Candidato cand in curso.selecionados)
                {
                    arqsaida.WriteLine($"{cand.NomeCandidato} {Math.Round(cand.MediaCandidato, 1)} {cand.NotaRedacao} {cand.NotaMat} {cand.NotaLinguas}");
                }
                arqsaida.WriteLine("Fila de Espera:");
                arqsaida.WriteLine(curso.filaEspera.Mostrar());
            }
        }
    }
}