using System;
using System.Collections.Generic;

namespace Projet_Info_Fondamentale
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Veuillez entrer le nombre de sommets");
            int n = Convert.ToInt32(Console.ReadLine());
            int[] tableau = new int[n];
            for (int i = 0; i < n; i++)
            {
                tableau[i] = i+1;
            }
            int[,] matrice = RemplissageAleatoire(tableau);*/
            //int[,] matrice = { { 1, 1, 0, 0, 1 }, { 1, 1, 1, 0, 0 }, { 0, 1, 1, 1, 0 }, { 0, 0, 1, 1, 1 }, { 1, 0, 0, 1, 1 } };
            //int[,] matrice = { { 1, 1, 0, 1 }, { 1, 1, 1, 0 }, { 0, 1, 1, 1 }, { 1, 0, 1, 1 } };
            int[,] matrice = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            AfficherMatrice(matrice); 
            int[,] degré = new int[matrice.GetLength(0), matrice.GetLength(1)];
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                {
                    degré[0, ligne] += matrice[ligne, colonne];
                }

                degré[1, ligne] = ligne;
                degré[0, ligne]--;
            }
            AfficherMatrice(degré);
            if (Possible2(degré))
            {
                Console.WriteLine("true");
                degré = Tri(degré);
                AfficherMatrice(degré);
                int[] coloration = new int[degré.GetLength(1)];
                coloration[0] = 1;
                int a = 1;
                coloration = Coloration2(matrice, degré, coloration,a);
                int[] temp = new int[coloration.Length];
                for(int i = 0; i< coloration.Length; i++)
                {
                    temp[degré[1, i]] = coloration[i];
                }
                for (int i = 0; i < coloration.Length; i++)
                {
                    Console.Write(" " + temp[i]);
                }
            }
            Console.ReadKey();
        }

        static int[] Coloration2(int[,] matrice, int[,] degré, int[] coloration ,int n)
        {
            if (n == degré.GetLength(1))
            {
                return coloration;
            }
            else
            {
                if (Voisin(matrice,degré,degré[1, n], coloration))
                {
                    coloration[n] = 2;
                }
                else
                {
                    coloration[n] = 1;
                }
                n++;
                return Coloration2(matrice, degré, coloration, n);
            }
            
            static bool Voisin(int[,] matrice,int[,] degré, int index, int[] coloration)
            {
                bool res = false;
                for(int i = 0; i < coloration.Length; i++)
                {
                    if(coloration[i] == 1 && matrice[degré[1,i],index] == 1)
                    {
                        res = true;
                    }
                }
                return res;
            }



        }

        public

        static bool Possible2(int[,] matrice)
        {
            int somme = 0;
            for (int ligne = 0; ligne < matrice.GetLength(1); ligne++)
            {
                somme += matrice[0,ligne];
            }
            if((somme / 2 == matrice.GetLength(0) && (somme/2)%2 == 0) || somme / 2 < matrice.GetLength(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static int[,] Tri(int[,] tableau)
        {
            bool res = false;
            int temp = 0;
            while (res == false)
            {
                res = true;
                for(int i = 0; i < tableau.GetLength(1) - 1; i++)
                {
                    if(tableau[0,i] < tableau[0, i + 1])
                    {
                        res = false;
                        temp = tableau[0, i];
                        tableau[0, i] = tableau[0, i + 1];
                        tableau[0, i + 1] = temp;
                        temp = tableau[1, i];
                        tableau[1, i] = tableau[1, i + 1];
                        tableau[1, i + 1] = temp;
                    }
                }
            }
            return tableau;
        }


        static int[,] RemplissageAleatoire(int[] tableau)
        {
            int[,] matrice = new int[tableau.Length, tableau.Length];
            var rand = new Random();
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    if(ligne == colonne)
                        {
                            matrice[ligne, colonne] = 1;
                        }
                    if(colonne > ligne)
                    {
                        matrice[ligne,colonne] = rand.Next(2);
                        matrice[colonne, ligne] = matrice[ligne, colonne];
                    }
                }
            }


            return matrice;
        }

        static void AfficherMatrice(int[,] matrice)
        {
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    Console.Write(" " + matrice[ligne, colonne]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

    }
}
