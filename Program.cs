using System;

namespace ConsoleApp
{
     class Program //se definește clasa Program
    {
          static void Main(string[] args) //metoda Main este punctul de intrare în aplicație
                                          //in care se afișează un meniu cu opțiuni disponibile pentru utilizator
        {
               Console.WriteLine("            Cereri HTTP");
               Console.WriteLine("                                        ");

               var methods = new Methods(); //se creează o instanță a clasei Methods,
                                            //care conține metodele pentru gestionarea cererilor HTTP

            while (true) //se utilizează o buclă while pentru a permite utilizatorului să selecteze
                         //și să efectueze diverse acțiuni în funcție de opțiunea aleasă.
            {
                    Console.WriteLine("1. Obtineti lista de categorii");
                    Console.WriteLine("2. Creati o noua categorie");
                    Console.WriteLine("3. Afisati detalii despre o categorie");
                    Console.WriteLine("4. Sterge o categorie");
                    Console.WriteLine("5. Modifică titlul unei categorii");
                    Console.WriteLine("6. Creaza un nou produs");
                    Console.WriteLine("7. Afisati produsele pentru o anumita categorie");
                    Console.WriteLine("8. Iesiti din program");

                    Console.Write("Introduceti optiunea: ");
                //În funcție de opțiunea aleasă de utilizator, se apelează metoda
                //corespunzătoare din clasa Methods pentru a efectua operațiunea dorită.
                var option = Console.ReadLine();

                    switch (option)
                    {
                         case "1":
                              methods.GetCategories();
                              break;
                         case "2":
                              methods.PostCategory();
                              break;
                         case "3":
                              methods.CategoryDetail();
                              break;
                         case "4":
                              methods.CategoryDelete();
                              break;
                         case "5":
                              methods.CategoryPut();
                              break;
                         case "6":
                              methods.PostProducts();
                              break;
                         case "7":
                              methods.GetProducts().Wait();
                              break;
                         case "8":
                              return;
                         default:
                              Console.WriteLine("Optiunea introdusa nu este valida. Va rugam sa reintroduceti optiunea.");
                              break;
                    }
               }
          }
     }
}
//Acest proiect demonstrează cum să se efectueze cereri HTTP către un API și să se gestioneze
//răspunsurile într-o manieră interactivă. Utilizatorul poate selecta diferite acțiuni, cum
//ar fi obținerea listei de categorii, crearea unei noi categorii, afișarea detaliilor unei
//categorii, ștergerea unei categorii, actualizarea numelui unei categorii, crearea unui nou
//produs și obținerea listei de produse pentru o anumită categorie.