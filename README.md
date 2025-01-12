

#### **Oyun Linki**
Oyununuza aşağıdaki linke tıklayarak ulaşabilirsiniz:  
**[Match Game - itch.io](https://niyazicancinkir.itch.io/match-game-latest)**  

---

### **Oyun Ekranları**  

#### **1. Başlangıç Ekranı (Objeler Spawn Edilmiş Durumda)**  
Aşağıdaki görüntü, objelerin rastgele olarak oyun alanına yerleştirildiği başlangıç ekranını göstermektedir:  
![Başlangıç Ekranı](https://github.com/user-attachments/assets/87412aeb-3931-42a7-a353-716bd0ff5082)

#### **2. Obje Yerleştirme (Oyun Alanına Objeyi Taşıma)**  
Bir objenin yerleştirme alanına sürüklenerek taşındığı durumu gösteren ekran görüntüsü:  
![Obje Yerleştirme](https://github.com/user-attachments/assets/9a7b80c3-ed4a-409a-91d4-c77fe0ea1ff4)  

---

### **Yetenekler ve Oyun Mekanikleri**

#### **1. "PATLAT" Yeteneği**  
- **Açıklama:** Oyun alanındaki tüm objeleri yok eder ve oyuncuya büyük miktarda puan kazandırır.  
- **Kullanım Durumu:** Oyuncu, hızlı bir şekilde ekranı temizlemek istediğinde bu yeteneği kullanabilir.  
![PATLAT Yeteneği](https://github.com/user-attachments/assets/108587f2-3c36-4061-b13f-4b5c86aee056)  

#### **2. "KARIŞTIR" Yeteneği**  
- **Açıklama:** Oyun alanındaki mevcut objeleri karıştırır ve yeniden rastgele şekilde spawn eder.  
- **Kullanım Durumu:** Oyuncu, objelerin yerleşimini değiştirmek ve yeni eşleşme fırsatları yaratmak istediğinde bu yeteneği kullanabilir.  
![KARIŞTIR Yeteneği](https://github.com/user-attachments/assets/ae711141-93f6-4f5e-bb9d-da47f91d43c6)

#### **3. "IPUCU" Yeteneği**  
- **Açıklama:** Oyuncuya ekrandaki eşleşen bir obje çiftinin yerini gösterir.  
- **Kullanım Durumu:** Oyuncunun eşleşmeleri bulmakta zorlandığı durumlarda yol gösterici olarak kullanılabilir.  
![IPUCU Yeteneği](https://github.com/user-attachments/assets/0e80dfce-25c8-423f-9174-a4ada26f6940)  

---

### **Ekstra Özellikler**  
- **Reset Butonu:** Oyun alanındaki tüm durumları sıfırlar ve oyunu baştan başlatır.  
- **Skor Sistemi:** Oyuncu, her eşleşme için **10 puan** kazanır.  

---

### **Oyun Çalışma Akışı**

1. **Objelerin Spawn Edilmesi:**  
   - Oyun başlangıcında asset store'dan alınmış olan objeler, oyun alanına rastgele şekilde eşleriyle birlikte spawn edilir.  

2. **Obje Taşıma ve Yerleştirme:**  
   - Oyuncu, taşımak istediği objeyi seçer ve sürükleyerek yerleştirme alanına taşır.  
   - Eğer:
     - **Alan Dolu ve Eşi Değilse:** Obje, yerleştirme alanından dışarı atılır.  
     - **Alan Boş ve Eşiyle Eşleşirse:** Objeler animasyon eşliğinde yok olur ve oyuncu puan kazanır.  

3. **Yerleştirme İşlemi:**  
   - Obje, boş bir alana yerleştirildiğinde **Lerp animasyonu** ile merkezine çekilir.

#### **Animasyon Kütüphanesi**  
Projede kullanılan animasyon kütüphanesi ekran görüntüsü:  
![Animasyon Kütüphanesi](https://github.com/user-attachments/assets/5298e0bb-4b16-4422-bc6e-cfce42983edd)

---

### **Sonuç**
Bu proje, kullanıcı etkileşimini ön planda tutarak sürükleyip bırakma, yetenek kullanımı ve dinamik oyun alanı mekanikleriyle eğlenceli bir deneyim sunmaktadır. Oyun, her yaş grubu için uygun olup strateji geliştirmeyi teşvik eder.
