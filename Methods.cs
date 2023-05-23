using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using static System.Net.WebRequestMethods;

namespace ConsoleApp
{
     public class Methods //Se definește clasa Methods, care conține metodele pentru
                          //efectuarea cererilor HTTP către API-ul web.
    {
          static readonly HttpClient client = new HttpClient(); //se definește o instanță a clasei HttpClient pentru a efectua cereri HTTP.
        static readonly string BASE_URL = "https://localhost:44370/api/"; //se definește adresa de bază a API-ului (BASE_URL) către care se vor efectua cererile.

        public void GetCategories() //aceasta metoda realizează o cerere HTTP GET către API pentru a obține lista de categorii.
                                    //Răspunsul este apoi procesat și afișat în consolă.
        {
               var response = client.GetAsync(BASE_URL + "Category/categories").Result;
               if (response.IsSuccessStatusCode)
               {
                    var categories = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                    foreach (var category in categories)
                    {
                         Console.WriteLine($"{category.id} - {category.name}");
                    }
               }
               else
               {
                    Console.WriteLine("Cererea a esuat cu codul de stare: " + response.StatusCode);
               }
          }

        //Metoda CategoryDetail primește de la utilizator un ID de categorie și efectuează o cerere HTTP GET pentru a obține detalii
        //despre categoria respectivă. Răspunsul este apoi afișat în consolă
        public void CategoryDetail()
          {
               Console.WriteLine("Introduceti ID-ul categoriei a cărei detalii vrei să vezi: ");
               var id = Console.ReadLine();
               var response = client.GetAsync(BASE_URL + $"Category/categories/{id}").Result;
               if (response.IsSuccessStatusCode)
               {
                    var categoryDetails = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                    Console.WriteLine(categoryDetails);
               }
               else
               {
                    Console.WriteLine("Cererea a esuat cu codul de stare: " + response.StatusCode);
               }
          }

        //Metoda PostCategory primește de la utilizator numele unei noi categorii și efectuează o cerere HTTP POST pentru a crea o nouă categorie. Dacă cererea
        //este reușită, se afișează un mesaj de succes și se obține lista actualizată de categorii.
        public void PostCategory()
          {
               Console.WriteLine("Introduceti numele noii categorii: ");
               var category_name = Console.ReadLine();
               var data = new { title = category_name };
               var response = client.PostAsJsonAsync(BASE_URL + "Category/categories", data).Result;
               if (response.IsSuccessStatusCode)
               {
                    Console.WriteLine("Cererea a fost indeplinita cu succes!");
                    GetCategories();
               }
               else
               {
                    Console.WriteLine("Cererea a esuat cu codul de stare: " + response.StatusCode);
               }
          }
        //Metoda CategoryDelete primește de la utilizator un ID de categorie și efectuează o cerere HTTP DELETE pentru
        //a șterge categoria respectivă.
        public void CategoryDelete()
          {
               Console.WriteLine("Introduceti ID-ul categoriei care vrei sa o stergi: ");
               var id = Console.ReadLine();
               var response = client.DeleteAsync(BASE_URL + $"Category/categories/{id}").Result;
               if (response.IsSuccessStatusCode)
               {
                    Console.WriteLine("Categoria a fost stearsa cu succes!");
               }
               else
               {
                    Console.WriteLine("Eroare la stergerea categoriei: " + response.StatusCode);
            }
        }
      // Metoda CategoryPut primește de la utilizator un ID de categorie și un nou nume și efectuează
      // o cerere HTTP PUT pentru a actualiza numele categoriei.
          public void CategoryPut()
        {
               Console.WriteLine("Introduceti ID-ul categoriei pe care vrei sa-l modifici: ");
               var id = Console.ReadLine();
               Console.WriteLine("Introduceti un nou nume pentru categorie: ");
               var category_name = Console.ReadLine();
               var data = new { title = category_name };
               var url = BASE_URL + $"Category/{id}";
               var response = client.PutAsJsonAsync(url, data).Result;
               if (response.IsSuccessStatusCode)
               {
                    Console.WriteLine("Categoria a fost modificata cu succes");
               }
               else
               {
                    Console.WriteLine("Eroare la modificarea categoriei: " + response.StatusCode);
               }
          }


        //Metoda PostProducts primește de la utilizator ID-ul unei categorii, numele și prețul unui produs și efectuează o
        //cerere HTTP POST pentru a crea un nou produs în categoria respectivă.
        public void PostProducts()
          {
               Console.WriteLine("Introduceti id-ul pentru categoria dorita: ");
               string id = Console.ReadLine();

               Console.WriteLine("Introduceti numele noului produs: ");
               string prodName = Console.ReadLine();

               Console.WriteLine("Introduceti pretul: ");
               decimal pret = Decimal.Parse(Console.ReadLine());

               var data = new
               {
                    title = prodName,
                    price = pret,
                    categoryId = id
               };

               string url = "https://localhost:44370/api/Category/categories/" + id + "/products";

               client.DefaultRequestHeaders.Accept.Clear();
               client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(url, data).Result;

               if (response.IsSuccessStatusCode)
               {
                    Console.WriteLine("Success");
               }
               else
               {
                    Console.WriteLine("Cererea a esuat cu codul de stare: " + response.StatusCode);
               }
          }

        //Metoda GetProducts primește de la utilizator ID-ul unei categorii și efectuează o cerere HTTP GET pentru a obține lista de produse asociate categoriei.
        //Lista de produse este apoi afișată în consolă.
        public async Task GetProducts()
          {
               Console.Write("Introduceti ID-ul pentru categoria dorita: ");
               string id = Console.ReadLine();

               string url = BASE_URL + $"Category/categories/{id}/products";
               using (var client = new HttpClient())
               {
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                         dynamic products = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                         foreach (var product in products)
                         {
                              Console.WriteLine(product);
                         }
                    }
                    else
                    {
                         Console.WriteLine("Cererea a eșuat cu codul de stare: {0}", response.StatusCode);
                    }
               }
          }

     }
}

//Metodele din clasa Methods utilizează clasa HttpClient pentru a efectua cereri HTTP GET,
//POST, PUT și DELETE către API-ul web. Aceste metode folosesc adresa de bază a
//API-ului (BASE_URL) și rutele specifice pentru fiecare operațiune.

//Răspunsurile primite de la API sunt procesate folosind JsonConvert pentru a parsa datele
//JSON și a le afișa sau a le utiliza în funcție de necesități. În cazul unui răspuns de
//succes, se afișează mesaje corespunzătoare pentru a confirma succesul operațiunii.
//În caz contrar, se afișează un mesaj de eroare împreună cu codul de stare al răspunsului HTTP.




