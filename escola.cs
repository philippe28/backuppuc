	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	namespace Application
	{
		class Aluno
		{
		public class aluno
	    {
			public int matricula ; 
			public string nome; 
			public string mae ; 
			public string pai;
			public int telefone ; 
			public string rua ;
			public int ruaN ;
			public string complemento;
			public string bairro;
			public string cidade;
			public string estado;
			public int cep;

	    }



			static public void Cadastro(aluno ficha)
		{
			Console.Clear();
			Console.Write("Matricula: ");
			ficha.matricula = int.Parse(Console.ReadLine());
			Console.Write("Nome do Aluno: ");
			ficha.nome = Console.ReadLine();
			Console.Write("Nome da Mae: ");
			ficha.mae = Console.ReadLine();
			Console.Write("Nome do Pai: ");
			ficha.pai = Console.ReadLine();
			Console.Write("Telefone: ");
			ficha.telefone = int.Parse(Console.ReadLine());
			Console.Write("Rua: ");
			ficha.rua = Console.ReadLine();
			Console.Write("N°: ");
			ficha.ruaN =int.Parse(Console.ReadLine());
			Console.Write("Complemento: ");
			ficha.complemento = Console.ReadLine();
			Console.Write("Bairro: ");
			ficha.bairro = Console.ReadLine();
			Console.Write("Cidade: ");
			ficha.cidade = Console.ReadLine();
			Console.Write("Estado: ");
			ficha.estado = Console.ReadLine();
			Console.Write("CEP: ");
			ficha.cep =int.Parse(Console.ReadLine());



	    


		}

			static void Main (string[] args)
		{
			string SN;
			int  opcao ,p,q,a,cont=0 ;
			Random r = new Random ();

			Dictionary<int,aluno> lista = new Dictionary<int, aluno> (100);
			Dictionary<int,aluno> lista2 = new Dictionary<int, aluno> (20);
		
			List <int> sorteio = new List<int>();
		
			Queue<aluno> fila = new Queue<aluno>();
			Queue<aluno> fila2 = new Queue<aluno>();


			do{

				Console.Clear();
				Console.WriteLine("1 – Cadastrar aluno");
				Console.WriteLine("2 – Imprimir lista de alunos");
				Console.WriteLine("3 – Imprimir lista de alunos na espera ");
				Console.WriteLine("4 – Pesquisar aluno");
				Console.WriteLine("5 – Desistência");
				Console.WriteLine("6 – Sorteio");
				Console.WriteLine("7 – Sair");


				 opcao = int.Parse (Console.ReadLine ());
				cont=0;

			



			switch (opcao) 
			{
			case 1:



				
				aluno ficha = new aluno ();



				if (lista.Count >100 && fila.Count >=0 )
				{
						Console.WriteLine("Lista de Espera");
					Cadastro (ficha);
					fila.Enqueue(ficha);

				}
					if (lista.Count<100 && fila.Count>0)
				{

					ficha=fila.Dequeue();
						sorteio.Add(ficha.matricula);
					lista.Add (ficha.matricula,ficha);

				}



				if (lista.Count <= 100 && fila.Count==0)
				{
					Cadastro (ficha);
						sorteio.Add(ficha.matricula);

					lista.Add (ficha.matricula, ficha);
						Console.ReadKey();
				}
				
				
				break;
			
		    
			case 2:
				
					Console.Clear();
					Console.WriteLine("2.1 - LISTAGEM DE SIMPLES");

					Console.WriteLine("2.2 - LISTAGEM DE ALUNOS COMPLETA");
					int opcao2 = int.Parse (Console.ReadLine ());


				if (opcao2 ==1)
				{
						Console.Clear();
						Console.WriteLine("LISTAGEM DE ALUNOS");

					foreach (KeyValuePair<int, aluno> o in lista) {

						Console.WriteLine("\n======================================");
						Console.Write ("\nMatricula: " + (o.Value).matricula + "\nNome Aluno:"+(o.Value).nome);

						cont++;
							if (cont==20)
							{
								cont=0;
								Console.ReadKey();
								Console.Clear();
							}


					
						}
				}
				else
				{
						Console.Clear();
						Console.WriteLine("LISTAGEM DE ALUNOS COMPLETA");




					foreach (KeyValuePair<int, aluno> o in lista) 
					{
						Console.WriteLine("\n======================================");
							Console.Write ("\nMatricula: " + (o.Value).matricula + "\nNome Aluno: " + (o.Value).nome + "\nNome Mae: " + (o.Value).mae + "\nNome do Pai: " + (o.Value).pai + "\nTelefone: " + (o.Value).telefone + "\nRua: " + (o.Value).rua + "\nN°: " + (o.Value).ruaN + "\nComplemento: " + (o.Value).complemento + "\nBairro: " + (o.Value).bairro + "\nCidade: " + (o.Value).cidade + "\nEstado: " + (o.Value).estado + "\nCep: " + (o.Value).cep);
					cont++;
							if (cont==3)
							{
								cont=0;
								Console.ReadKey();
								Console.Clear();
							}


						}


					
				}
					Console.ReadKey();
					break;
					case 3:
					Console.Clear();
					Console.WriteLine("LISTAGEM DE ALUNOS NA ESPERA");
						Console.WriteLine("\n======================================");
					fila2=fila;
					for (int i=0;i<fila.Count;i++)
					{
						ficha=fila2.Dequeue();
					lista2.Add (ficha.matricula,ficha);

					}

					foreach (KeyValuePair<int, aluno> o in lista2) {


						Console.Write ("\nMatricula: " + (o.Value).matricula + "\nNome Aluno:"+(o.Value).nome);
						cont++;
							if (cont==20)
							{
								cont=0;
								Console.ReadKey();
								Console.Clear();
							}
					}
					Console.ReadKey();
					break;

				case 4:
					Console.Clear();
					Console.WriteLine("Informe a matricula para procurar : ");
					 p = int.Parse(Console.ReadLine());
					Console.Clear();
					fila2=fila;
					for (int i=0;i<fila.Count;i++)
					{
						ficha=fila2.Dequeue();
					lista2.Add (ficha.matricula,ficha);

					}



					if (lista2.ContainsKey(p)==false && lista.ContainsKey(p)==false)
					{
				Console.WriteLine("Não existe a matricula informada!");
						Console.ReadKey();
					}

						foreach (KeyValuePair<int, aluno> o in lista) {

							if ((o.Value).matricula==(p))
							{
							Console.Clear();
						Console.Write ("\nMatricula: " + (o.Value).matricula + "\nNome Aluno: " + (o.Value).nome + "\nNome Mae: " + (o.Value).mae + "\nNome do Pai: " + (o.Value).pai + "\nTelefone: " + (o.Value).telefone + "\nRua: " + (o.Value).rua + "\nN°: " + (o.Value).ruaN + "\nComplemento: " + (o.Value).complemento + "\nBairro: " + (o.Value).bairro + "\nCidade: " + (o.Value).cidade + "\nEstado: " + (o.Value).estado + "\nCep: " + (o.Value).cep);
								Console.ReadKey();
							}
					}



					if (lista2.ContainsKey(p)==true)
						{
						Console.WriteLine("Aluno esta na lista de espera");
						Console.ReadKey();
					}

						

				    break;

				case 5:
					Console.Clear();

					Console.WriteLine("Informe a matricula para procurar : ");
					p = int.Parse(Console.ReadLine());
					Console.Clear();
					if (lista2.ContainsKey(p)==false && lista.ContainsKey(p)==false)
					{
				Console.WriteLine("Não existe a matricula informada!");
					}

					if (lista.ContainsKey(p)==true)
						{
						Console.WriteLine("Tem certeza que deseja apagar [s] ou [n]");

						SN=Console.ReadLine();
						if (SN=="S" ||SN =="s")
						{
						lista.Remove(p);

							sorteio.Remove(sorteio.BinarySearch(p));
						Console.WriteLine("Foi removido com sucesso");

						}
						else
						{
						Console.WriteLine("Cancelado");
						}
						Console.ReadKey();
					}
					fila2=fila;
					for (int i=0;i<fila.Count;i++)
					{
						ficha=fila2.Dequeue();
					lista2.Add (ficha.matricula,ficha);

					}

					foreach (KeyValuePair<int, aluno> o in lista2) {


						Console.Write ("\nMatricula: " + (o.Value).matricula + "\nNome Aluno:"+(o.Value).nome);

					}


					if (lista2.ContainsKey(p)==true)
						{
						Console.WriteLine("Aluno esta na lista de espera");
					}

						

				    break;


					case 6:
					Console.Clear();
					Console.WriteLine("Quantas bolsa seram sorteadas");
					q=int.Parse(Console.ReadLine());
					Console.Clear();





					for (int i =0,j=q;i<q;i++,j--)
				{
					a = (r.Next (lista.Count));
					

					

					foreach (KeyValuePair<int, aluno> o in lista)
					{

							if ((o.Value).matricula == (sorteio[a]))
							{
								if (sorteio.Contains(a)== true)
								{
									j++;
								}
							Console.WriteLine("\n======================================");
						Console.Write ("\nMatricula: " + (o.Value).matricula + "\nNome Aluno: " + (o.Value).nome + "\nNome Mae: " + (o.Value).mae + "\nNome do Pai: " + (o.Value).pai + "\nTelefone: " + (o.Value).telefone + "\nRua: " + (o.Value).rua + "\nN°: " + (o.Value).ruaN + "\nComplemento: " + (o.Value).complemento + "\nBairro: " + (o.Value).bairro + "\nCidade: " + (o.Value).cidade + "\nEstado: " + (o.Value).estado + "\nCep: " + (o.Value).cep);
							
								Console.ReadKey();
							

							}
					}

				}

					break;






			}	


			}while(opcao!=7);

				







				}
	}
	}
