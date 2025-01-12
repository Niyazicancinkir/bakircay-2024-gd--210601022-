Oyunumun Tanıtım Videosunu Aşağıdaki youtube linkinden ulaşabilirsiniz.
https://niyazicancinkir.itch.io/match-game-latest
https://youtu.be/YSzh4QnupwQ

Oyun Ekranı Objeler spawn edilmişken böyle gözükmektedir.
![image](https://github.com/user-attachments/assets/6c09eb5f-90e6-4c3f-a8d6-dfb71abee1e6)

Oyun Ekrani bir objeyi yerleştirme alanına yerleştirdim.
![image](https://github.com/user-attachments/assets/61377456-07c2-4a3e-b670-e8b2dd788b34)

Proje, kullanıcıların oyun alanındaki objeleri sürükleyip bırakmalarına olanak tanıyan bir Match game'in ilksel yapısının sistemini oluşturmayı hedeflemiştir. Bu sistemi uygularken, objelerin belirtilen alan dışına çıkması durumunda otomatik olarak merkezde spawn edilmesi sağlanmıştır. Ayrıca, eğer placement area alanına bir obje yerleştirmiş isek diğer obje alandan dışarı atılır. 

Oyun Çalışma Akışı 

asset storedan indirmiş olduğum assetler oyun alanında rastgele bir biçimde eşlenikleri ile birlikte spawn edilir.
taşımak istediğim objenin üzerine tıklayıp sürükleyerek yerleştirme alanına götürülür.
eğer o alan dolu ise obje o alan dışına atılır.
boş ise merkezine doğru Lerp ile çekilir.

![image](https://github.com/user-attachments/assets/21c96cb5-0d46-473c-8d08-3abbbccb1abb)

 
