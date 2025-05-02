
# 📚 Bibliophile Web API

**Bibliophile**, kitapseverler için geliştirilen bir sosyal kitap platformudur. Bu repository, uygulamanın .NET tabanlı backend API katmanını içerir. API; kitaplar, yazarlar, blog yazıları, kullanıcı listeleri ve beğeni sistemleri gibi pek çok özelliği yönetir.

---

## 🚀 Özellikler

- 🔐 JWT tabanlı kimlik doğrulama ve yetkilendirme
- 📚 Kitap arama, listeleme ve filtreleme
- 🧑‍🎨 Yazar bilgilerini yönetme
- 🏷️ Tür bazlı kategori ve filtreleme
- 📝 Kullanıcıların blog yazıları oluşturması
- 📋 Kitap listeleri oluşturma ve yönetme
- ⭐ Kitap puanlama ve yorum yapma
- 👍 Blog yazısı, kitap ve liste beğeni sistemi

---

## 🛠️ Kullanılan Teknolojiler

- **.NET 9**
- **Entity Framework Core**
- **MySQL**
- **ASP.NET Identity**
- **JWT Bearer Authentication**
- **AutoMapper**
- **Swagger**

---

## 📡 API Uç Noktaları (Özet)

API'nin detaylı uç noktaları README'nin devamında tablo halinde sunulmuştur. Aşağıda bazı öne çıkan endpoint'ler örneklenmiştir:

- `/api/Book` → Kitap listeleme
- `/api/Author` → Yazar yönetimi
- `/api/Genre` → Tür filtreleme
- `/api/BlogPost` → Blog yazıları
- `/api/BookList` → Kitap listeleri
- `/api/Rating` → Puanlama & yorumlar
- `/api/Account/register` → Kullanıcı kaydı

---

## 🔑 Yetkilendirme Rolleri

| Rol       | Açıklama                             |
|-----------|--------------------------------------|
| **User**  | Giriş yapmış kullanıcı               |
| **Write** | Blog yazısı yazabilen kullanıcı      |
| **Admin** | Tüm işlemleri yapabilen yönetici     |

---

## 🧑‍💻 Kurulum ve Çalıştırma

1. Projeyi klonlayın:
```bash
git clone https://github.com/yourusername/Bibliophile.git
cd Bibliophile/Backend
```

2. Bağımlılıkları yükleyin:
```bash
dotnet restore
```

3. `appsettings.json` dosyasını yapılandırın:
```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=bibliophile;user=root;password=yourpassword"
}
```

4. Veritabanını güncelleyin:
```bash
dotnet ef database update
```

5. Uygulamayı başlatın:
```bash
dotnet run
```

6. Swagger UI'a erişin: `https://localhost:5001/swagger`

---

## 📚 API Kullanımı

1. `/api/Account/register` ile yeni kullanıcı oluştur
2. `/api/Account/login` ile token al
3. Swagger UI'da `Authorize` butonuna tıklayarak token'ı `Bearer your_token` formatında gir
4. Tüm korumalı uç noktalara erişim sağla

---

## 📂 Ek Bilgiler

- Tüm uç noktalar detaylı şekilde README içerisinde listelenmiştir
- Swagger kullanarak test edebilir veya Postman gibi HTTP client araçlarıyla entegrasyon sağlayabilirsiniz

---


