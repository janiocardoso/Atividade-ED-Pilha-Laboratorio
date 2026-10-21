using System;

public class Pilha
{
    private int[] item;
    private int topo;

    public Pilha(int capacidade)
    {
        item = new int[capacidade];
        topo = 0;
    }

    public bool vazia()
    {
        return topo == 0;
    }

    public bool cheia()
    {
        return topo == item.Length;
    }

    public int tamanho()
    {
        return topo;
    }

    public void empilha(int valor)
    {
        if (cheia())
            throw new Exception("Erro: pilha cheia");
        
        item[topo] = valor;
        topo++;
    }

    public int desempilha()
    {
        if (vazia())
            throw new Exception("Erro: pilha vazia");
        
        topo--;
        return item[topo];
    }

    public int consultaTopo()
    {
        if (vazia())
            throw new Exception("Erro: pilha vazia");
        
        return item[topo - 1];
    }

    public void mostra()
    {
        Console.Write("[");
        for (int i = 0; i < topo; i++)
        {
            Console.Write(item[i]);
            if (i < topo - 1)
                Console.Write(", ");
        }
        Console.WriteLine("]");
    }
}

public class SolucaoLaboratorio
{
    public static void Main()
    {
        Console.WriteLine($"Inversão de \"PILHA\": {inverte("PILHA")}");
        Console.WriteLine($"Expressão {{[()]}} balanceada: {balanceado("{[()]}")}");

        int[] vetorOriginal = { 5, 1, 4, 2, 8 };
        int[] vetorOrdenado = bubblePilhas(vetorOriginal, out int totalComparacoes);
        
        Console.WriteLine($"Vetor ordenado: [{string.Join(", ", vetorOrdenado)}]");
        Console.WriteLine($"Comparações: {totalComparacoes}");

        Pilha pilhaDesordenada = new Pilha(vetorOriginal.Length);
        foreach (int numero in vetorOriginal)
        {
            pilhaDesordenada.empilha(numero);
        }

        ordenaPilha(pilhaDesordenada);
        Console.Write("Pilha ordenada (topo primeiro): [");
        while (!pilhaDesordenada.vazia())
        {
            Console.Write(pilhaDesordenada.desempilha());
            if (!pilhaDesordenada.vazia())
            {
                Console.Write(", ");
            }
        }
        Console.WriteLine("]");
    }

    public static string inverte(string texto)
    {
        Pilha pilhaCaracteres = new Pilha(texto.Length);
        foreach (char caractere in texto)
        {
            pilhaCaracteres.empilha(caractere);
        }
        
        char[] textoInvertido = new char[texto.Length];
        for (int i = 0; i < texto.Length; i++)
        {
            textoInvertido[i] = (char)pilhaCaracteres.desempilha();
        }
        return new string(textoInvertido);
    }

    public static bool balanceado(string expressao)
    {
        Pilha pilhaFechamento = new Pilha(expressao.Length);
        
        foreach (char caractere in expressao)
        {
            if (caractere == '(' || caractere == '[' || caractere == '{')
            {
                pilhaFechamento.empilha(caractere);
            }
            else if (caractere == ')' || caractere == ']' || caractere == '}')
            {
                if (pilhaFechamento.vazia()) return false;
                
                char topoAbertura = (char)pilhaFechamento.desempilha();
                
                if ((caractere == ')' && topoAbertura != '(') ||
                    (caractere == ']' && topoAbertura != '[') ||
                    (caractere == '}' && topoAbertura != '{'))
                {
                    return false;
                }
            }
        }
        return pilhaFechamento.vazia();
    }

    public static int[] bubblePilhas(int[] vetorEntrada, out int comparacoes)
    {
        comparacoes = 0;
        int quantidade = vetorEntrada.Length;
        
        if (quantidade == 0) return vetorEntrada;

        Pilha pilhaA = new Pilha(quantidade);
        Pilha pilhaB = new Pilha(quantidade);
        Pilha pilhaResultado = new Pilha(quantidade);

        for (int i = 0; i < quantidade; i++)
        {
            pilhaA.empilha(vetorEntrada[i]);
        }

        for (int elementosRestantes = quantidade; elementosRestantes >= 1; elementosRestantes--)
        {
            int bolha = pilhaA.desempilha();
            
            for (int j = 0; j < elementosRestantes - 1; j++)
            {
                int valorAtual = pilhaA.desempilha();
                comparacoes++;
                
                if (bolha > valorAtual)
                {
                    pilhaB.empilha(valorAtual);
                }
                else
                {
                    pilhaB.empilha(bolha);
                    bolha = valorAtual;
                }
            }
            pilhaResultado.empilha(bolha);
            
            Pilha temp = pilhaA;
            pilhaA = pilhaB;
            pilhaB = temp;
        }

        int[] vetorOrdenado = new int[quantidade];
        for (int i = 0; i < quantidade; i++)
        {
            vetorOrdenado[i] = pilhaResultado.desempilha();
        }
        return vetorOrdenado;
    }

    public static Pilha ordenaPilha(Pilha pilhaOriginal)
    {
        Pilha pilhaAuxiliar = new Pilha(pilhaOriginal.tamanho());

        while (!pilhaOriginal.vazia())
        {
            int valorTemporario = pilhaOriginal.desempilha();

            while (!pilhaAuxiliar.vazia() && pilhaAuxiliar.consultaTopo() > valorTemporario)
            {
                pilhaOriginal.empilha(pilhaAuxiliar.desempilha());
            }
            pilhaAuxiliar.empilha(valorTemporario);
        }

        while (!pilhaAuxiliar.vazia())
        {
            pilhaOriginal.empilha(pilhaAuxiliar.desempilha());
        }

        return pilhaOriginal;
    }
}