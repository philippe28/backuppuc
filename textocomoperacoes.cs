using System;
using System.Collections;
using System.Collections.Generic;
using AED;
using System.IO;
using System.Text;

namespace Application
{
    class MainClass
    {
		// Classes que implementam estruturas de dados básicas: Lista (simples e duplamente encadeada), Fila e Pilha.
// Última modificação: 21/04/2013 - revisão de todas as classes e métodos
// Autor: Rodrigo Richard Gomes


    #region Classe CCelula - representa a célula utilizada pelas classes CLista, CFila e CPilha
    /// <summary>
    /// Classe utilizada pelas classes CLista, CFila e CPilha
    /// </summary>
    class CCelula
    {
        public Object Item; // O Item armazendo pela célula
        public CCelula Prox; // Referencia a próxima célula

        /// <summary>
        /// Inicializa uma nova instância da classe CCelula atribuindo null aos atributos Item e Prox.
        /// </summary>
        public CCelula()
        {
            Item = null;
            Prox = null;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe CCelula atribuindo o valor passado por parâmetro ao atributo Item e null ao atributo Prox.
        /// </summary>
        /// <param name="ValorItem">Valor a ser armazenado pela célula.</param>
        public CCelula(object ValorItem)
        {
            Item = ValorItem;
            Prox = null;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe CCelula atribuindo ValorItem ao atributo Item e ProxCelula ao atributo Prox.
        /// </summary>
        /// <param name="ValorItem">Valor a ser armazenado pela célula</param>
        /// <param name="ProxCelula">Referência para a próxima célula.</param>
        public CCelula(object ValorItem, CCelula ProxCelula)
        {
            Item = ValorItem;
            Prox = ProxCelula;
        }
    }
    #endregion

    #region Classe CCelulaDup - representa a célula utilizada pela classe CListaDup
    /// <summary>
    /// Classe utilizada pela classe CListaDup
    /// </summary>
    class CCelulaDup
    {

        public Object Item; // O Item armazendo pela célula
        public CCelulaDup Ant; // Referencia a célula anterior
        public CCelulaDup Prox; // Referencia a próxima célula

        /// <summary>
        /// Inicializa uma nova instância da classe CCelulaDup atribuindo null aos atributos Item, Ant e Prox.
        /// </summary>
        public CCelulaDup()
        {
            Item = null;
            Ant = null;
            Prox = null;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe CCelula atribuindo o valor passado por parâmetro ao atributo Item e null aos atributos Ant e Prox.
        /// </summary>
        /// <param name="ValorItem">Valor a ser armazenado pela célula.</param>
        public CCelulaDup(object ValorItem)
        {
            Item = ValorItem;
            Ant = null;
            Prox = null;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe CCelula atribuindo ValorItem ao atributo Item e ProxCelular ao atributo Prox.
        /// </summary>
        /// <param name="ValorItem">Valor a ser armazenado pela célula.</param>
        /// <param name="celulaAnt">Referência para a célula anterior.</param>
        /// <param name="ProxCelula">Referência para a próxima célula.</param>
        public CCelulaDup(object ValorItem,
                          CCelulaDup celulaAnt,
                          CCelulaDup proxCelula)
        {
            Item = ValorItem;
            Ant = celulaAnt;
            Prox = proxCelula;
        }
    }
    #endregion

    #region Classe CLista - Lista encadeada (simples) com célula cabeça
    /// <summary>
    /// Implementa uma lista encadeada com célula cabeça.
    /// </summary>
    class CLista
    {
        private CCelula Primeira; // Referencia a célula cabeça 
        private CCelula Ultima; // Referencia a última célula da lista
        private int Qtde = 0;

        /// <summary>
        /// Função construtora. Aloca a célula cabeça e faz todas as referências apontarem para ela.
        /// </summary>
        public CLista()
        {
            Primeira = new CCelula();
            Ultima = Primeira;
        }

        /// <summary>
        /// Verifica se a lista está vazia.
        /// </summary>
        /// <returns>Retorna TRUE se a lista estiver vazia e FALSE se ela tiver elementos.</returns>
        public bool Vazia()
        {
            return Primeira == Ultima;
        }

        /// <summary>
        /// Insere um novo Item no fim da lista.
        /// </summary>
        /// <param name="ValorItem">O Item a ser inserido.</param>
        public void InsereFim(Object ValorItem)
        {
            Ultima.Prox = new CCelula(ValorItem);
            Ultima = Ultima.Prox;
            Qtde++;
        }

        /// <summary>
        /// Insere um novo Item no começo da lista.
        /// </summary>
        /// <param name="ValorItem">O Item a ser inserido.</param>
        public void InsereComeco(Object ValorItem)
        {
            Primeira.Prox = new CCelula(ValorItem, Primeira.Prox);
            if (Primeira.Prox.Prox == null)
                Ultima = Primeira.Prox;
            Qtde++;
        }

        /// <summary>
        /// Insere o Item passado por parâmetro na posição determinada.
        /// </summary>
        /// <param name="ValorItem">O Item a ser inserido na lista.</param>
        /// <param name="Posicao">Posição na qual o elemento será inserido. O primeiro elemento está na posição 1, e assim por diante.</param>
        /// <returns>Se a posição existir e o método conseguir inserir o elemento, retorna TRUE. Caso a posição não exista, retorna FALSE</returns>
        public bool InsereIndice(Object ValorItem, int Posicao)
        {
            // Verifica se a posição passada por parâmetro é uma posição válida, ou seja, no intervalo entre 1 e Qtde+1
            if (Posicao >= 1 && Posicao <= Qtde + 1)
            {
                int i = 0;
                CCelula aux = Primeira;
                // Procura a posição a ser inserido
                while (i < Posicao - 1)
                {
                    aux = aux.Prox;
                    i++;
                }
                aux.Prox = new CCelula(ValorItem, aux.Prox);
                if (aux.Prox.Prox == null) // se a célula inserida está na última posição.
                    Ultima = aux.Prox;
                Qtde++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Imprime todos os elementos da lista usando o comando while
        /// </summary>
        public void Imprime(bool ItemPorLinha)
        {
            CCelula aux = Primeira.Prox;
            while (aux != null)
            {
                Console.WriteLine(aux.Item);
                aux = aux.Prox;
            }
        }

        /// <summary>
        /// Imprime todos os elementos da lista usando o comando for
        /// </summary>
        public void ImprimeFor()
        {
            for (CCelula aux = Primeira.Prox; aux != null; aux = aux.Prox)
                Console.Write(aux.Item);
        }

        /// <summary>
        /// Imprime todos os elementos simulando formato de lista: [/]->[7]->[21]->[13]->null
        /// </summary>
        public void ImprimeFormatoLista()
        {
            Console.Write("[/]->");
            for (CCelula aux = Primeira.Prox; aux != null; aux = aux.Prox)
                Console.Write("[" + aux.Item.ToString() + "]->");
            Console.WriteLine("null");
        }

        /// <summary>
        /// Verifica se o Item passado como parâmetro está contido na lista.
        /// </summary>
        /// <param name="elemento">O Item a ser localiado.</param>
        /// <returns>Retorna TRUE caso o Item esteja presente na lista.</returns>
        public bool Contem(Object elemento)
        {
            bool achou = false;
            CCelula aux = Primeira.Prox;
            while (aux != null && !achou)
            {
                achou = aux.Item.Equals(elemento);
                aux = aux.Prox;
            }
            return achou;
        }

        /// <summary>
        /// Verifica se o Item passado como parâmetro está contido na lista. (Obs: usa o comando FOR)
        /// </summary>
        /// <param name="elemento">O Item a ser localiado.</param>
        /// <returns>Retorna TRUE caso o Item esteja presente na lista.</returns>
        public bool ContemFor(Object elemento)
        {
            bool achou = false;
            for (CCelula aux = Primeira.Prox; aux != null && !achou; aux = aux.Prox)
                achou = aux.Item.Equals(elemento);
            return achou;
        }

        /// <summary>
        /// Remove e retorna o primeiro Item da lista (remoção física, ou seja, elimina a célula que contém o elemento).
        /// </summary>
        /// <returns>Um Object contendo o Item removido ou null caso a lista esteja vazia.</returns>
        public Object RemoveRetornaComeco()
        {
            // Verifica se há elementos na lista
            if (Primeira != Ultima)
            {
                CCelula aux = Primeira.Prox;
                Primeira.Prox = aux.Prox;
                if (Primeira.Prox == null) // Se a célula cabeça está apontando para null, significa que o único elemento da lista foi removido
                    Ultima = Primeira;
                Qtde--;
                return aux.Item;
            }
            return null;
        }

        /// <summary>
        /// Remove e retorna o primeiro Item da lista (remoção lógica, ou seja, remove a célula cabeça fazendo com que a célula seguinte ela se torne a nova célula cabeça).
        /// </summary>
        /// <returns>Um Object contendo o Item removido ou null caso a lista esteja vazia.</returns>
        public Object RemoveRetornaComecoSimples()
        {
            // Verifica se há elementos na lista
            if (Primeira != Ultima)
            {
                Primeira = Primeira.Prox;
                Qtde--;
                return Primeira.Item;
            }
            return null;
        }

        /// <summary>
        /// Remove o primeiro Item da lista fazendo com que a célula seguinte à célula cabeça se torne a nova célula cabeça. Não retorna o Item removido.
        /// </summary>
        public void RemoveComecoSemRetorno()
        {
            if (Primeira != Ultima)
            {
                Primeira = Primeira.Prox;
                Qtde--;
            }
        }

        /// <summary>
        /// Remove o último Item da lista.
        /// </summary>
        /// <returns>Um Object contendo o Item removido ou null caso a lista esteja vazia.</returns>
        public Object RemoveRetornaFim()
        {
            if (Primeira != Ultima)
            {
                CCelula aux = Primeira;
                while (aux.Prox != Ultima)
                    aux = aux.Prox;

                CCelula aux2 = aux.Prox;
                Ultima = aux;
                Ultima.Prox = null;
                Qtde--;
                return aux2.Item;
            }
            return null;
        }

        /// <summary>
        /// Remove o último Item da lista sem retorná-lo.
        /// </summary>
        public void RemoveFimSemRetorno()
        {
            if (Primeira != Ultima)
            {
                CCelula aux = Primeira;
                while (aux.Prox != Ultima)
                    aux = aux.Prox;

                Ultima = aux;
                Ultima.Prox = null;
                Qtde--;
            }
        }

        /// <summary>
        /// Localiza o Item passado por parâmetro e o remove da Lista
        /// </summary>
        /// <param name="ValorItem">Item a ser removido da lista.</param>
        public void Remove(Object ValorItem)
        {
            if (Primeira != Ultima)
            {
                CCelula aux = Primeira;
                bool achou = false;
                while (aux.Prox != null && !achou)
                {
                    achou = aux.Prox.Item.Equals(ValorItem);
                    if (!achou)
                        aux = aux.Prox;
                }
                if (achou) // achou o elemento
                {
                    aux.Prox = aux.Prox.Prox;
                    if (aux.Prox == null)
                        Ultima = aux;
                    Qtde--;
                }
            }
        }

        /// <summary>
        /// Remove o elemento na posição passada como parâmetro.
        /// </summary>
        /// <param name="Posicao">A posição a ser removida. OBS: o primeiro elemento está na posição 1, e assim por diante.</param>
        /// <returns>Retorna TRUE se a posição existe e foi removida com sucesso, e FALSE caso a posição não exista.</returns>
        public bool RemoveIndice(int Posicao)
        {
            // Se é uma posição válida e a lista possui elementos
            if ((Posicao >= 1) && (Posicao <= Qtde) && (Primeira != Ultima))
            {
                int i = 0;
                CCelula aux = Primeira;
                while (i < Posicao - 1)
                {
                    aux = aux.Prox;
                    i++;
                }
                aux.Prox = aux.Prox.Prox;
                if (aux.Prox == null)
                    Ultima = aux;
                Qtde--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove e retorna o elemento na posição passada como parâmetro.
        /// </summary>
        /// <param name="Posicao">A posição a ser removida. OBS: o primeiro elemento está na posição 1, e assim por diante.</param>
        /// <returns>Um object contendo o elemento removido da lista. Caso a posição seja inválida, retorna null.</returns>
        public Object RemoveRetornaIndice(int Posicao)
        {
            // Se é uma posição válida e a lista possui elementos
            if ((Posicao >= 1) && (Posicao <= Qtde) && (Primeira != Ultima))
            {
                int i = 0;
                CCelula aux = Primeira;
                while (i < Posicao - 1)
                {
                    aux = aux.Prox;
                    i++;
                }
                CCelula aux2 = aux.Prox;
                aux.Prox = aux.Prox.Prox;
                if (aux.Prox == null)
                    Ultima = aux;
                Qtde--;
                return aux2.Item;
            }
            return null;
        }

        /// <summary>
        /// Retorna o primeiro Item da lista.
        /// </summary>
        /// <returns>Um Object contendo o primeiro Item da lista. Se a lista estiver vazia ou pos estiver posicionado em uma posição inválida a função retorna null.</returns>
        public Object RetornaPrimeiro()
        {
            if (Primeira != Ultima)
                return Primeira.Prox.Item;
            else
                return null;
        }

        /// <summary>
        /// Retorna o último Item da lista.
        /// </summary>
        /// <returns>Um Object contendo o último Item da lista. Se a lista estiver vazia ou pos estiver posicionado em uma posição inválida a função retorna null.</returns>
        public Object RetornaUltimo()
        {
            if (Primeira != Ultima)
                return Ultima.Item;
            else
                return null;
        }

        /// <summary>
        /// Retorna o Item contido na posição p da lista.
        /// </summary>
        /// <param name="p">A posição desejada. A primeira posição da lista é a posição 1.</param>
        /// <returns>Um Object contendo o Item da posição p da lista.</returns>
        public Object RetornaIndice(int Posicao)
        {
            // EXERCÍCIO : deve retornar o elemento da posição p passada por parâmetro
            // [cabeça]->[7]->[21]->[13]->null
            // retornaIndice(2) deve retornar o elemento 21. retornaIndice de uma posiçao inexistente deve retornar null.
            // Se é uma posição válida e a lista possui elementos
            if ((Posicao >= 1) && (Posicao <= Qtde) && (Primeira != Ultima))
            {
                int i = 1;
                CCelula aux = Primeira.Prox;
                // Procura a posição a ser inserido
                while (i < Posicao)
                {
                    aux = aux.Prox;
                    i++;
                }
                return aux.Item;
            }
            return null;
        }

        /// <summary>
        /// Função que retorna a quantidade de itens da lista.
        /// </summary>
        /// <returns>Quantidade de itens da lista.</returns>
        public int Quantidade()
        {
            return Qtde;
        }

        /// <summary>
        /// Torna possível iterar sobre a CLista usando o comando foreach
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            for (CCelula aux = Primeira.Prox; aux != null; aux = aux.Prox)
                yield return aux.Item;
        }
    }
    #endregion

    #region Classe CFila - Fila (ou lista FIFO: first-in first-out)
    /// <summary>
    /// Classe que representa uma Fila (ou lista FIFO: first-in first-out)
    /// </summary>
    class CFila
    {
        private CCelula Frente; // Referencia a primeira célula da CFila (Célula cabeça)
        private CCelula Tras; // Referencia a última célula da CFila
        private int Qtde = 0;

        /// <summary>
        /// Função construtora. Cria a célula cabeça e faz as referências Frente e Tras apontarem para ela.
        /// </summary>
        public CFila()
        {
            Frente = new CCelula();
            Tras = Frente;
        }

        /// <summary>
        /// Verifica se a fila está vazia
        /// </summary>
        /// <returns>Retorna TRUE se a lista estiver vazia e FALSE caso contrário.</returns>
        public bool Vazia()
        {
            return Frente == Tras;
        }

        /// <summary>
        /// Insere um novo Item no fim da fila.
        /// </summary>
        /// <param name="ValorItem">Um Object contendo o elemento a ser inserido no final da fila.</param>
        public void Enfileira(Object ValorItem)
        {
            Tras.Prox = new CCelula(ValorItem);
            Tras = Tras.Prox;
            Qtde++;
        }

        /// <summary>
        /// Retira e retorna o primeiro elemento da fila.
        /// </summary>
        /// <returns>Um Object contendo o primeiro elemento da fila. Caso a fila esteja vazia retorna null.</returns>
        public Object Desenfileira()
        {
            Object Item = null;
            if (Frente != Tras)
            {
                Frente = Frente.Prox;
                Item = Frente.Item;
                Qtde--;
            }
            return Item;
        }

        /// <summary>
        /// Retorna o primeiro Item da fila sem removê-lo.
        /// </summary>
        /// <returns>Um Object contendo o primeiro Item da fila.</returns>
        public Object Peek()
        {
            if (Frente != Tras)
                return Frente.Prox.Item;
            else
                return null;
        }

        /// <summary>
        /// Verifica se o Item passado como parâmetro está contido na lista.
        /// </summary>
        /// <param name="elemento">Um object contendo o Item a ser localiado.</param>
        /// <returns>Retorna TRUE caso o Item esteja presente na lista.</returns>
        public bool Contem(Object elemento)
        {
            bool achou = false;
            CCelula aux = Frente.Prox;
            while (aux != null && !achou)
            {
                achou = aux.Item.Equals(elemento);
                aux = aux.Prox;
            }
            return achou;
        }

        /// <summary>
        /// Verifica se o Item passado como parâmetro está contido na lista. (Obs: usa o comando FOR)
        /// </summary>
        /// <param name="elemento">Um object contendo o Item a ser localiado.</param>
        /// <returns>Retorna TRUE caso o Item esteja presente na lista.</returns>
        public bool ContemFor(Object elemento)
        {
            bool achou = false;
            for (CCelula aux = Frente.Prox; aux != null && !achou; aux = aux.Prox)
                achou = aux.Item.Equals(elemento);
            return achou;
        }

        /// <summary>
        /// Função que retorna a quantidade de itens da fila.
        /// </summary>
        /// <returns>Quantidade de itens da fila.</returns>
        public int Quantidade() //Função
        {
            return Qtde;
        }

        /// <summary>
        /// Torna possível iterar sobre a CFila usando o comando foreach
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            for (CCelula aux = Frente.Prox; aux != null; aux = aux.Prox)
                yield return aux.Item;
        }
    }
    #endregion

    #region Classe CPilha - CPilha (ou lista LIFO: last-in first-out)
    /// <summary>
    /// Classe que representa uma Pilha (ou lista LIFO: last-in first-out)
    /// </summary>
    class CPilha
    {
        private CCelula Topo = null;
        private int Qtde = 0;

        /// <summary>
        /// Função construtora.
        /// </summary>
        public CPilha()
        {
            // nenhum código
        }

        /// <summary>
        /// Verifica se a Pilha está vazia.
        /// </summary>
        /// <returns>Retorna TRUE se a PILHA estiver vazia e FALSE caso contrário.</returns>
        public bool Vazia()
        {
            return Topo == null;
        }

        /// <summary>
        /// Insere o novo Item no Topo da Pilha
        /// </summary>
        /// <param name="ValorItem">Um Object contendo o Item a ser inserido no Topo da Pilha.</param>
        public void Empilha(Object ValorItem)
        {
            CCelula aux = Topo;
            Topo = new CCelula(ValorItem);
            Topo.Prox = aux;
            Qtde++;
        }

        /// <summary>
        /// Retira e retorna o Item do Topo da Pilha.
        /// </summary>
        /// <returns>Um Object contendo o Item retirado do Topo da Pilha. Caso a Pilha esteja vazia retorna null.</returns>
        public Object Desempilha()
        {
            Object Item = null;
            if (Topo != null)
            {
                Item = Topo.Item;
                Topo = Topo.Prox;
                Qtde--;
            }
            return Item;
        }

        /// <summary>
        /// Verifica se o Item passado como parâmetro está contido na lista.
        /// </summary>
        /// <param name="elemento">Um object contendo o Item a ser localiado.</param>
        /// <returns>Retorna TRUE caso o Item esteja presente na lista.</returns>
        public bool Contem(Object elemento)
        {
            bool achou = false;
            CCelula aux = Topo;
            while (aux != null && !achou)
            {
                achou = aux.Item.Equals(elemento);
                aux = aux.Prox;
            }
            return achou;
        }

        /// <summary>
        /// Verifica se o Item passado como parâmetro está contido na lista. (Obs: usa o comando FOR)
        /// </summary>
        /// <param name="elemento">Um object contendo o Item a ser localiado.</param>
        /// <returns>Retorna TRUE caso o Item esteja presente na lista.</returns>
        public bool ContemFor(Object elemento)
        {
            bool achou = false;
            for (CCelula aux = Topo; aux != null && !achou; aux = aux.Prox)
                achou = aux.Item.Equals(elemento);
            return achou;
        }

        /// <summary>
        /// Retorna o Item do Topo da Pilha sem removê-lo.
        /// </summary>
        /// <returns>Um Object contendo o Item do Topo da Pilha. Caso a Pilha esteja vazia retorna null.</returns>
        public Object Peek()
        {
            if (Topo != null)
                return Topo.Item;
            else
                return null;
        }

        /// <summary>
        /// Função que retorna a quantidade de itens da Pilha.
        /// </summary>
        /// <returns>Quantidade de itens da Pilha.</returns>
        public int Quantidade() //Função
        {
            return Qtde;
        }
		/// <summary>
		/// limpa pilha.
		/// </summary>
		  public Object limpa() //Função
        {
			 Object Item = null;
            if (Topo != null)
            {
				while ( Topo != null)
				{

                Item = Topo.Item;
                Topo = Topo.Prox;
                
				}
            }
			Qtde =0;
            return Item;

            
        }

        /// <summary>
        /// Torna possível iterar sobre a CPilha usando o comando foreach
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            for (CCelula aux = Topo; aux != null; aux = aux.Prox)
                yield return aux.Item;
        }

    }
    #endregion

    #region Classe CListaDup - Lista duplamente encadeada com célula cabeça
    /// <summary>
    /// Implementa uma lista duplamente encadeada.
    /// </summary>
    class CListaDup
    {
        private CCelulaDup Primeira; // Referencia a primeira célula da lista (célula cabeça)
        private CCelulaDup Ultima; // Referencia a última célula da lista 
        private int Qtde = 0;

        /// <summary>
        /// Aloca a célula cabeça e faz todas as referências
        /// apontarem para ela.
        /// </summary>
        public CListaDup()
        {
            Primeira = new CCelulaDup();
            Ultima = Primeira;
        }

        /// <summary>
        /// Verifica se a lista está vazia.
        /// </summary>
        /// <returns>Retorna true se a lista estiver vazia.</returns>
        public bool Vazia()
        {
            return Primeira == Ultima;
        }

        /// <summary>
        /// Insere um novo elemento no fim da lista.
        /// </summary>
        /// <param name="ValorItem">O Item a ser inserido no final da lista.</param>
        public void InsereFim(Object ValorItem)
        {
            Ultima.Prox = new CCelulaDup(ValorItem, Ultima, null);
            Ultima = Ultima.Prox;
            Qtde++;
        }

        /// <summary>
        /// Insere um novo elemento no começo da lista.
        /// </summary>
        /// <param name="ValorItem">O Item a ser inserido no começo da lista.</param>
        public void InsereComeco(Object ValorItem)
        {
            if (Primeira == Ultima) // Se a lista estiver vazia insere no fim
            {
                Ultima.Prox = new CCelulaDup(ValorItem, Ultima, null);
                Ultima = Ultima.Prox;
            }
            else // senão insere no começo
            {
                Primeira.Prox = new CCelulaDup(ValorItem, Primeira, Primeira.Prox);
                Primeira.Prox.Ant = Primeira.Prox;
            }
            Qtde++;
        }

        /// <summary>
        /// Insere o Item passado por parâmetro na posição determinada.
        /// </summary>
        /// <param name="ValorItem">O Item a ser inserido na lista.</param>
        /// <param name="Posicao">Posição na qual o elemento será inserido. O primeiro elemento está na posição 1, e assim por diante.</param>
        /// <returns>Se a posição existir, retorna TRUE. Caso a posição não exista, retorna FALSE</returns>
        public bool InsereIndice(Object ValorItem, int Posicao)
        {
            // Verifica se a posição passada por parâmetro é uma posição válida, ou seja, no intervalo entre 1 e Qtde+1
            if (Posicao >= 1 && Posicao <= Qtde + 1)
            {
                CCelulaDup aux = Primeira;
                // Procura a posição a ser inserido
                for (int i = 0; i < Posicao - 1; aux = aux.Prox, i++) ;
                aux.Prox = new CCelulaDup(ValorItem, aux, aux.Prox);
                if (aux.Prox.Prox != null) // se a célula inserida não é a última
                    aux.Prox.Prox.Ant = aux.Prox;
                else // se a célula é a última
                    Ultima = aux.Prox;
                Qtde++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Insere o número passado por parâmetro em ordem crescente.
        /// </summary>
        /// <param name="ValorItem">O número inteiro a ser inserido.</param>
        public void InsereIntOrdenado(Object ValorItem)
        {
            if (Primeira == Ultima)
            {
                Primeira.Prox = new CCelulaDup(ValorItem, Primeira, null);
                Ultima = Ultima.Prox;
            }
            else
            {
                CCelulaDup aux;
                for (aux = Primeira.Prox; (aux != null) && (int)(aux.Item) < (int)ValorItem; aux = aux.Prox) ;
                if (aux == null) // a lista acabou e não foi encontrado Item maior que o ValorItem a ser inserido
                {
                    Ultima.Prox = new CCelulaDup(ValorItem, Ultima, null);
                    Ultima = Ultima.Prox;
                }
                else // Um Item < ValorItem foi encontrado. Inserir ValorItem antes dele
                {
                    CCelulaDup novaCelula = new CCelulaDup(ValorItem, aux.Ant, aux);
                    novaCelula.Ant.Prox = novaCelula;
                    aux.Ant = novaCelula;
                }
            }
            Qtde++;
        }

        /// <summary>
        /// Remove o primeiro elemento da lista. Na verdade, remove a referência para a célula cabeça, e torna a primeira célula na nova célula cabeça
        /// </summary>
        public void RemoveComecoSemRetorno()
        {
            if (Primeira != Ultima)
            {
                Primeira = Primeira.Prox;
                Primeira.Ant = null;
                Qtde--;
            }
        }

        /// <summary>
        /// Remove o último elemento da lista. Na verdade, remove as referências para a última célula, forçando que o Garbage Collector desaloque a última célula
        /// </summary>
        public void RemoveFimSemRetorno()
        {
            if (Primeira != Ultima)
            {
                Ultima = Ultima.Ant;
                Ultima.Prox = null;
                Qtde--;
            }
        }

        /// <summary>
        /// Remove o elemento na posição passada como parâmetro.
        /// </summary>
        /// <param name="Posicao">A posição a ser removida.</param>
        /// <returns>Retorna TRUE se a posição existe e foi removida com sucesso, e FALSE caso a posição não exista.</returns>
        public bool RemoveIndice(int Posicao)
        {
            // Se é uma posição válida e a lista possui elementos
            if ((Posicao >= 1) && (Posicao <= Qtde) && (Primeira != Ultima))
            {
                CCelulaDup aux = Primeira;
                for (int i = 0; i < Posicao - 1; aux = aux.Prox, i++) ;
                aux.Prox = aux.Prox.Prox;
                if (aux.Prox != null)
                    aux.Prox.Ant = aux;
                else
                    Ultima = aux;
                Qtde--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove e retorna o elemento na posição passada como parâmetro.
        /// </summary>
        /// <param name="Posicao">A posição a ser removida.</param>
        /// <returns>Um object contendo o elemento removido da lista.</returns>
        public Object RemoveRetornaIndice(int Posicao)
        {
            // Se é uma posição válida e a lista possui elementos
            if ((Posicao >= 1) && (Posicao <= Qtde) && (Primeira != Ultima))
            {
                CCelulaDup aux = Primeira;
                for (int i = 0; i < Posicao - 1; aux = aux.Prox, i++) ;
                CCelulaDup aux2 = aux.Prox;
                aux.Prox = aux2.Prox;
                if (aux2 != null)
                    aux2.Ant = aux;
                else
                    Ultima = aux;
                Qtde--;
                return aux2.Item;
            }
            return null;
        }

        /// <summary>
        /// Localiza o Item passado por parâmetro e o remove da Lista
        /// </summary>
        /// <param name="ValorItem">Item a ser removido da lista.</param>
        public void Remove(Object ValorItem)
        {
            if (Primeira != Ultima)
            {
                CCelulaDup aux = Primeira.Prox;
                bool achou = false;
                while (aux != null && !achou)
                {
                    achou = aux.Item.Equals(ValorItem);
                    if (!achou)
                        aux = aux.Prox;
                }
                if (achou) // achou o elemento
                {
                    CCelulaDup anterior = aux.Ant;
                    CCelulaDup proximo = aux.Prox;
                    anterior.Prox = proximo;
                    if (proximo != null)
                        proximo.Ant = anterior;
                    else
                        Ultima = anterior;
                    Qtde--;
                }
            }
        }

        /// <summary>
        /// Remove e retorna o primeiro elemento da lista.
        /// </summary>
        /// <returns>Um object contendo o primeiro elemento da lista.</returns>
        public Object RemoveRetornaComeco()
        {
            if (Primeira != Ultima)
            {
                CCelulaDup aux = Primeira.Prox;
                Primeira = Primeira.Prox;
                Primeira.Ant = null;
                Qtde--;
                return aux.Item;
            }
            else
                return null;
        }

        /// <summary>
        /// Remove e retorna o último elemento da lista.
        /// </summary>
        /// <returns>Um object contendo o último elemento da lista.</returns>
        public Object RemoveRetornaFim()
        {
            if (Primeira != Ultima)
            {
                CCelulaDup aux = Ultima;
                Ultima = Ultima.Ant;
                Ultima.Prox = null;
                Qtde--;
                return aux.Item;
            }
            else
                return null;
        }

        /// <summary>
        /// Imprime todos os elementos da lista duplamente encadeada usando o comando while.
        /// </summary>
        public void Imprime()
        {
            CCelulaDup aux = Primeira.Prox;
            while (aux != null)
            {
                Console.WriteLine(aux.Item);
                aux = aux.Prox;
            }
        }

        /// <summary>
        /// Imprime todos os elementos da lista duplamente encadeada usando o comando for.
        /// </summary>
        public void ImprimeFor()
        {
            for (CCelulaDup aux = Primeira.Prox; aux != null; aux = aux.Prox)
                Console.WriteLine(aux.Item);
        }

        /// <summary>
        /// Imprime todos os elementos da lista duplamente encadeada em sentido inverso usando o comando while.
        /// </summary>
        public void ImprimeInv()
        {
            CCelulaDup aux = Ultima;
            while (aux.Ant != null)
            {
                Console.WriteLine(aux.Item);
                aux = aux.Ant;
            }
        }

        /// <summary>
        /// Imprime todos os elementos da lista duplamente encadeada em sentido inverso usando o comando for.
        /// </summary>
        public void ImprimeInvFor()
        {
            for (CCelulaDup aux = Ultima; aux.Ant != null; aux = aux.Ant)
                Console.WriteLine(aux.Item);
        }

        /// <summary>
        /// Verifica se o Item passado como parâmetro está contido na lista.
        /// </summary>
        /// <param name="elemento">Um object contendo o Item a ser localiado.</param>
        /// <returns>Retorna TRUE caso o Item esteja presente na lista.</returns>
        public bool Contem(Object elemento)
        {
            bool achou = false;
            CCelulaDup aux = Primeira.Prox;
            while (aux != null && !achou)
            {
                achou = aux.Item.Equals(elemento);
                aux = aux.Prox;
            }
            return achou;
        }

        /// <summary>
        /// Verifica se o Item passado como parâmetro está contido na lista. (Obs: usa o comando FOR)
        /// </summary>
        /// <param name="elemento">Um object contendo o Item a ser localiado.</param>
        /// <returns>Retorna TRUE caso o Item esteja presente na lista.</returns>
        public bool ContemFor(Object elemento)
        {
            bool achou = false;
            for (CCelulaDup aux = Primeira.Prox; aux != null && !achou; aux = aux.Prox)
                achou = aux.Item.Equals(elemento);
            return achou;
        }

        /// <summary>
        /// Retorna o primeiro elemento da lista.
        /// </summary>
        /// <returns>Um object contendo o primeiro elemento da lista ou null caso a lista esteja vazia.</returns>
        public Object RetornaPrimeiro()
        {
            if (Primeira != Ultima)
                return Primeira.Prox.Item;
            return null;
        }

        /// <summary>
        /// Retorna o Item contido na posição p da lista.
        /// </summary>
        /// <param name="p">A posição desejada. A Primeira posição da lista é a posição 1.</param>
        /// <returns>Um Object contendo o Item da posição p da lista.</returns>
        public Object RetornaIndice(int Posicao)
        {
            // EXERCÍCIO : deve retornar o elemento da posição p passada por parâmetro
            // [cabeça]->[7]->[21]->[13]->null
            // retornaIndice(2) deve retornar o elemento 21. retornaIndice de uma posiçao inexistente deve retornar null.
            // Se é uma posição válida e a lista possui elementos
            if ((Posicao >= 1) && (Posicao <= Qtde) && (Primeira != Ultima))
            {
                CCelulaDup aux = Primeira.Prox;
                // Procura a posição a ser inserido
                for (int i = 1; i < Posicao; i++, aux = aux.Prox) ;
                if (aux != null)
                    return aux.Item;
            }
            return null;
        }

        /// <summary>
        /// Retorna o elemento da última posição.
        /// </summary>
        /// <returns>Um object contendo o último elemento da lista ou null caso a lista esteja vazia.</returns>
        public Object RetornaUltimo()
        {
            if (Primeira != Ultima)
                return Ultima.Item;
            return null;
        }

        /// <summary>
        /// Função que retorna a quantidade de elementos da lista.
        /// </summary>
        /// <returns>Quantidade de elementos da lista.</returns>
        public int Quantidade()
        {
            return Qtde;
        }

        /// <summary>
        /// Torna possível iterar sobre a CListaDup usando o comando foreach
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            for (CCelulaDup aux = Primeira.Prox; aux != null; aux = aux.Prox)
                yield return aux.Item;
        }

        /// <summary>
        /// Permite iterar sobre uma CListaDup de forma invertida usando o comando foreach
        /// </summary>
        /// Exemplo de uso: foreach(Object x in LD.Reverse)
        public IEnumerable Reverse
        {
            get
            {
                for (CCelulaDup aux = Ultima; aux != Primeira; aux = aux.Ant)
                    yield return aux.Item;
            }
        }

    }
    #endregion

	
        static double fatorial(double n)
        {
            for (double j = n - 1; j >= 1; j--)
                n *= j;
            		return n;
        }

        public static void Main (string[] args)
		{

			string LerConta;            
			CPilha s = new CPilha ();
			int cont = 0, opcao;
			double popD, ass;
			object popS;
			CPilha NumeroPilha = new CPilha ();
			CPilha Simbolos = new CPilha ();
			Console.WriteLine ("Escolha a opçao\n[1] Texto com operaçoes \n[2] Faça sua operação");
			opcao=int.Parse(Console.ReadLine());

				StreamReader ler = new StreamReader ("expressoes.txt");
			if (opcao == 1) {
				LerConta = ler.ReadLine ();
			}
			else
				LerConta = Console.ReadLine();

			StreamWriter Resultados = new StreamWriter("RESULTADOS.txt");

            while (LerConta != null)
            {                       
                {
					foreach (char item in LerConta)
                    {
						if (item.Equals(' ')) ;
                        else if (item.Equals('(')) ;
                        else if (item.Equals('+'))
                            Simbolos.Empilha(item);
                        else if (item.Equals('-'))
                            Simbolos.Empilha(item);
                        else if (item.Equals('*'))
                            Simbolos.Empilha(item);
                        else if (item.Equals('/'))
                            Simbolos.Empilha(item);
                        else if (item.Equals('^'))
                            Simbolos.Empilha(item);
                        else if (item.Equals('!'))
                            Simbolos.Empilha(item);
						else if (item.Equals('.'))
                            Simbolos.Empilha(item);
                        else if (item.Equals(')'))
                        {
                            popS = Simbolos.Desempilha();
                            popD = double.Parse(NumeroPilha.Desempilha().ToString());
                            if (popS.Equals('+')) popD = Convert.ToDouble(NumeroPilha.Desempilha().ToString()) + popD;
                            else if (popS.Equals('-')) popD = Convert.ToDouble(NumeroPilha.Desempilha().ToString()) - popD;
                            else if (popS.Equals('*')) popD = Convert.ToDouble(NumeroPilha.Desempilha().ToString()) * popD;
                            else if (popS.Equals('/')) popD = Convert.ToDouble(NumeroPilha.Desempilha().ToString()) / popD;
                            else if (popS.Equals('^')) popD = Math.Pow(Convert.ToDouble(NumeroPilha.Desempilha().ToString()), popD);
                            else if (popS.Equals('!')) popD = fatorial(popD);	
							else if (popS.Equals('.')) popD = Convert.ToDouble(NumeroPilha.Desempilha().ToString()) +(popD / Math.Pow(10,(popD.ToString().Length)));
                            NumeroPilha.Empilha(popD);
                        }

                        else if (cont >= 1)
                        {
                            s.Empilha(item);
                            popD = Convert.ToDouble(NumeroPilha.Desempilha().ToString()) *10  ;
                            cont++;
                            ass = Convert.ToDouble(s.Desempilha().ToString()) + popD;
                            NumeroPilha.Empilha(ass);
                        }
                        
                        else
                        {                            
                            NumeroPilha.Empilha(item);
                            cont++;
                        }

						if (item.Equals('1') || item.Equals('2') || item.Equals('3') || item.Equals('4') || item.Equals('5') || item.Equals('6') || item.Equals('7') || item.Equals('8') || item.Equals('9') || item.Equals('0'));
                         else cont = 0;
                                               
                    }
                }

                Resultados.WriteLine("{0} = {1}", LerConta, NumeroPilha.Desempilha());
              	if (opcao != 1)
					break;
				if (NumeroPilha.Quantidade() == 0)
					LerConta = ler.ReadLine();
				else
					Simbolos.Empilha(")");

            }

			 ler.Close();
            Resultados.Close();

        }
    }
}