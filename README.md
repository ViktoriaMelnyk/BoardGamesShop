BoardGamesShop

Projekt z przedmiotu "Programowanie w środowisku ASP.NET"
![Animation](https://user-images.githubusercontent.com/72021306/156077264-65dae9d7-cc98-4050-bde9-a93c56c727c3.gif)


Aby uruchomić projekt należy: 
1) ściągnąć repozytorium na lokalny dysk twardy; 
2) w pliku appsettings.json zmienic nazwę serwera: 

"ConnectionStrings": {
    "DefaultConnection": "Server= W tym miejscu ma być nazwa serwera; Database =BoardGamesShopDb; Trusted_Connection=True; MultipleActiveResultSets=True"
  },
 3) uruchomić projekt

Dane do logowania się jako administrator:
Email: admin@blabla.com
Hasło: Admin12!

Tools: ASP.NET Core MVC (.NET6), Visual Studio 2022, SSMS 2018

Register/Login

Role: Admin, Pracownik, Konsument, Przedsiębiorca.

Domyślnie urzytkownik może zarejestrować się jako konsument, inne role przydziela Administrator

![Register](https://user-images.githubusercontent.com/72021306/156079186-5c1a20e8-3897-4b4e-8504-d4fae058af4c.gif)

Kategorie


![Kategorie](https://user-images.githubusercontent.com/72021306/156184722-51c0e942-ae91-4fc9-a11d-0f38b474673e.gif)

Lista gier

![Gry](https://user-images.githubusercontent.com/72021306/156184800-a295ed01-10d0-411b-92fc-95e1636f9875.gif)

Firmy

![firmy](https://user-images.githubusercontent.com/72021306/156193103-fbfc069a-5bc2-4404-9abf-0a8d4d0ce0ac.gif)

Koszyk

Cena się zmienia odnośnie ilości

![Koszyk](https://user-images.githubusercontent.com/72021306/156193174-21beba08-6ab2-480f-a93b-517b3df0bb7e.gif)

