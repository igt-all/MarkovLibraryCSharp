


/// <summary> *!* main *!* </summary>
/// <returns> *!* int *!* </returns>
/// <remarks> *!* *optional* Write remarks for main here. *!* </remarks>
/// <example> *!* *optional* example usage. *!* </example>
/// <exception cref="type"> *!* *optional if no exceptions thrown* description of criteria for throwing exception. *!* </exception>
namespace MarkovLibraryCSharp
{
	internal class Program
	{
		private static void Main()
		{
			int initialnumSpins = 8;  //Initial number of Free spins
			int retriggernumSpins = 0;  // Retrigger free spins
			int maxSpins = 300; // Max free spins
			double pRetrigger = 0;
			string anotherRun = "y";
			while (anotherRun == "y" || anotherRun == "Y")
			{

				var probability = markovPlayWSS(initialnumSpins, retriggernumSpins, maxSpins, pRetrigger);
				Console.WriteLine(probability);

				Console.WriteLine("Do another run? y/n ");
				//cin >> anotherRun;
			}

		}
		int Max(int x, int y)
		{
			return ((x > y) ? x : y);
		}

		//Returns min
		int Min(int x, int y)
		{
			return ((x > y) ? y : x);
		}
		/// <summary> *!* markov play *!* </summary>
		/// <param name="scatPay"> *!* scat pay parameter. *!* </param>
		/// <param name="pays"> *!* pays parameter. *!* </param>
		/// <param name="fout"> *!* fout parameter. *!* </param>
		/// <remarks> *!*  Finds average pay for each line given the weighted average pays *!* </remarks>
		static double markovPlayWSS(int initialnumSpins, int retriggernumSpins, int maxSpins, double pRetrigger)
		{

			pRetrigger = 0.00260363321417124000;//0.01333149492017420000;
			double p2 = 0.000116711325743665000;// 0.01057155297532660000;
			double p3 = 0.000004291990876288470;// 0.00293185776487663000;
			double p4 = 0.000008218475621;//SS 3trigger
			double p5 = 0.000000000934407;//SS 4trigger
			double p6 = 0.000000000000032;//SS 5trigger


			double pNoretrigger = 0;//={0.9841940036601510};
			pNoretrigger = 1 - (pRetrigger + p2 + p3);
			double pNoretrigger2 = 0;//={0.9841940036601510};
			pNoretrigger2 = 1 - (p4 + p5 + p6);
			int i = 0;
			retriggernumSpins = 8;
			int retriggernumSpins2 = 16;
			int retriggernumSpins3 = 32;
			//maxSpins = 100;
			//initialnumSpins = 3;

			TransitionGraph<int, int> TG= new TransitionGraph<int, int>(); // Class Transition Graph Plus

			int spin;

			for (spin = 0; spin < maxSpins + 1; spin++)
			{ //For every free spin
			  //Add a vertex			
				TG.addVertex(spin);//Vertex contains #spin on, mult level in, scat collected, and pays			
			}
			TG.setVertexProb(initialnumSpins, 1);// Set starting point with prob 1

			for (spin = 1; spin < (maxSpins - retriggernumSpins3); spin++)
			{
				if ((spin - 1) == 0)
				{
					TG.addArc(spin, spin - 1, pNoretrigger2);
					TG.addArc(spin, spin + retriggernumSpins - 1, p4);
					TG.addArc(spin, spin + retriggernumSpins2 - 1, p5);
					TG.addArc(spin, spin + retriggernumSpins3 - 1, p6);
				}
				else
				{
					TG.addArc(spin, spin - 1, pNoretrigger);

					TG.addArc(spin, spin + retriggernumSpins - 1, pRetrigger);
					TG.addArc(spin, spin + retriggernumSpins2 - 1, p2);
					TG.addArc(spin, spin + retriggernumSpins3 - 1, p3);
				}
			}
			///Special Cases
			for (spin = (maxSpins - retriggernumSpins); spin < maxSpins + 1; spin++)
			{
				TG.addArc(spin, spin - 1, pNoretrigger);
				TG.addArc(spin, maxSpins, pRetrigger);
				TG.addArc(spin, maxSpins, p2);
				TG.addArc(spin, maxSpins, p3);
			}




			double avgSpins = 0;

	



			for (i = 1; i < maxSpins; i++)
			{
				TG.markovProbability();
				avgSpins += i * TG.getVertexProb(0);
			}

			double lastprob = 0;


			TG.markovProbability();
			for (i = 0; i < maxSpins + 1; i++)
			{

				lastprob += TG.getVertexProb(i);
			}
			Console.WriteLine($"Prob to get to max free spins: {lastprob}") ;
			avgSpins += (maxSpins) * lastprob;

			//printf("\20%15s%12f\n","Ave Spins:",avgSpins);
			//cout << "More Precise: " << avgSpins << endl;

			TG.resetVertexValues();
			TG.clearArcSet();
			return avgSpins;

		}

		
		
	}
}