Oyunumunu link'e tıklayarak ulaşabilirsiniz.
https://niyazicancinkir.itch.io/match-game-latest

Oyun Ekranı Objeler spawn edilmişken böyle gözükmektedir.
![image](https://github.com/user-attachments/assets/87412aeb-3931-42a7-a353-716bd0ff5082)


Oyun Ekrani bir objeyi yerleştirme alanına yerleştirdim.
![image](https://github.com/user-attachments/assets/9a7b80c3-ed4a-409a-91d4-c77fe0ea1ff4)

3 adet yetenek kullanılmıştır :
1: "PATLAT" : Tüm objeleri yok eder kullanıcıya puan kazandıran güçlü bir yetenektir.
![image](https://github.com/user-attachments/assets/108587f2-3c36-4061-b13f-4b5c86aee056)
2: "KARIŞTIR" Ekranda kalan objeleri karıştırıp yeniden spawn eder
![image](https://github.com/user-attachments/assets/ae711141-93f6-4f5e-bb9d-da47f91d43c6)
3: "IPUCU" Ekrandan oyuncuya ipucu veren bir yetenektir. Eş objelerden bir çiftinin yerini gösterir.
![image](https://github.com/user-attachments/assets/0e80dfce-25c8-423f-9174-a4ada26f6940)
"Reset" bu buton oyundaki tüm her şeyi sıfırlar ve oyun yeniden başlar.
"Skor" oyuncu her eşleştirdiği obje için 10 puan kazanır.

Proje, kullanıcıların oyun alanındaki objeleri sürükleyip bırakmalarına olanak tanıyan bir Match game oyunudur. Eşlenik olanları bulduğunuzda objelerden puan kazanırsınız.

Oyun Çalışma Akışı 

asset storedan indirmiş olduğum assetler oyun alanında rastgele bir biçimde eşlenikleri ile birlikte spawn edilir.
taşımak istediğim objenin üzerine tıklayıp sürükleyerek yerleştirme alanına götürülür.

eğer o alan dolu ise ve eşi değil ise  obje o alan dışına atılır.
eşleniği geldiğinde animason ile yok olur.
boş ise merkezine doğru Lerp ile çekilir.

![image](https://github.com/user-attachments/assets/21c96cb5-0d46-473c-8d08-3abbbccb1abb)

 
Animasyon kütüphanesi:
![Screenshot 2025-01-11 215448](https://github.com/user-attachments/assets/5298e0bb-4b16-4422-bc6e-cfce42983edd)
 
