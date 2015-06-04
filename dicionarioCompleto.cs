using System;

namespace Dicionary
{
	public class CDicionario
	{

		class CCelulaDicionario
	{
		// Atributos
			public Object key, value;
			public CCelulaDicionario prox;
			// Construtora que anula os três atributos da célula
		public CCelulaDicionario()
		{
				key = null;
				value = null;
                prox = null;

		}

						// Construtora que inicializa key e value com os argumentos passados
		// por parâmetro e anula a referência à próxima célula
		public CCelulaDicionario(Object chave, Object valor)
		{
				key = chave;
				value = valor;
                prox = null;

		}
		// Construtora que inicializa todos os atribulos da célula com os argumentos
		// passados por parâmetro
		public CCelulaDicionario(Object chave, Object valor, CCelulaDicionario proxima)
		{
				key = chave;
				value=valor;
                prox = proxima;

		}
	}
		private CCelulaDicionario primeira, ultima;
		private int Qtde = 0;

		public CDicionario()
		{
			primeira = new CCelulaDicionario();
            ultima = primeira;
		}
		public bool Vazio()
		{
				return primeira == ultima;
		}
		public void Adiciona(Object chave, Object valor)
		{
			
				 bool achou = false;
		    for (CCelulaDicionario aux = primeira.prox; aux != null && !achou; aux = aux.prox)
					achou = aux.key.Equals(chave);

				if (achou == false)
				{


				  ultima.prox = new CCelulaDicionario (chave,valor);
          		  ultima = ultima.prox;            
          		  Qtde++;
				Console.WriteLine(chave);
			Console.WriteLine(valor);
				}


				 
			 



		}
		public Object RecebeValor(Object chave)
		{
			bool achou = false;
		    for (CCelulaDicionario aux = primeira.prox; aux != null && !achou; aux = aux.prox)
				{achou = aux.key.Equals(chave);
					return (object)aux.value;
				}
				return null;
		}

		public static void Main (string[] args)
		{

			CDicionario url = new CDicionario();

			url.Add("ping www.google.com", "74.125.234.81");
			url.Add("ping www.pucminas.br", "74.125.224.64");
			url.Add("ping www.gmail.com", "74.139.219.93");
			url.Add("ping www.youtube.com", "74.139.222.13");
			url.Add("ping www.capes.gov.br", "74.126.232.23");
			url.Add("ping www.yahoo.com", "74.139.257.25");
			url.Add("ping www.microsoft.com", "74.113.289.13");
			url.Add("ping www.www.twitter.com", "74.150.222.13");
			url.Add("ping www.brasil.gov.br", "74.139.458.83");
			url.Add("ping www.wikipedia.com", "74.139.202.03");
			url.Add("ping www.amazon.com", "74.139.226.33");
			url.Add("ping research.microsoft.com", "74.139.250.53");
			url.Add("ping www.facebook.com", "74.139.219.79");
			url.Add("ping www.whitehouse.gov", "74.139.502.87");
			url.Add("ping www.answers.com", "74.139.282.55");
			url.Add("ping www.uol.com.br", "200.221.2.45");
			url.Add("ping www.hotmail.com", "74.189.282.83");
			url.Add("ping www.cplusplus.com", "74.137.272.03");
			url.Add("ping www.nyt.com", "74.385.782.03");
			url.Add("ping mangareader.com.br", "74.111.252.15");
			url.Add("ping www.testosterona.blog.br", "209.239.113.31");
			url.Add("ping www.umsabadoqualquer.com", "769.163.129.192");
			url.Add("ping www.willtirando.com.br", "184.168.17.1");
			url.Add("ping vidadeprogramador.com.br", "187.45.207.81");
		
		
			Console.ReadKey(); foreach (string k in url.Keys)
			{
				Console.WriteLine("Chave {0} {1}", k, url[k]);
			}
				Console.WriteLine("Informe a chave para procurar : ");
				string p = Console.ReadLine();
				Console.WriteLine(url[p].ToString());
				Console.ReadKey();





		}
	}
		}