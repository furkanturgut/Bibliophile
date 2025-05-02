
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
## Api EndPointler
## 🔐 Kimlik Doğrulama

| Metod | Endpoint               | Açıklama                                      |
|-------|------------------------|-----------------------------------------------|
| POST  | `/api/Account/register`| Yeni kullanıcı kaydı                          |
| POST  | `/api/Account/login`   | Kullanıcı girişi                              |
| GET   | `/api/Account/me`      | Giriş yapmış kullanıcının bilgilerini getirir |

---

## 📚 Kitaplar

| Metod | Endpoint                   | Açıklama                                       |
|-------|----------------------------|------------------------------------------------|
| GET   | `/api/Book`                | Tüm kitapları listeler                        |
| GET   | `/api/Book/{id}`           | ID'ye göre kitap detaylarını getirir          |
| POST  | `/api/Book`                | Yeni kitap ekler (Admin)                      |
| PUT   | `/api/Book/{id}`           | Kitap bilgilerini günceller (Admin)           |
| DELETE| `/api/Book/{id}`           | Kitabı siler (Admin)                          |
| GET   | `/api/Book/search?q=...`   | Kitapları ada göre arar                       |
| GET   | `/api/Book/genre/{genreId}`| Türe göre kitapları listeler                  |
| GET   | `/api/Book/author/{authorId}`| Yazara göre kitapları listeler               |

---

## 👨‍🎨 Yazarlar

| Metod | Endpoint                 | Açıklama                                  |
|-------|--------------------------|-------------------------------------------|
| GET   | `/api/Author`            | Tüm yazarları listeler                    |
| GET   | `/api/Author/{id}`       | ID'ye göre yazar detaylarını getirir      |
| POST  | `/api/Author`            | Yeni yazar ekler (Admin)                  |
| PUT   | `/api/Author/{id}`       | Yazar bilgilerini günceller (Admin)       |
| DELETE| `/api/Author/{id}`       | Yazarı siler (Admin)                      |
| GET   | `/api/Author/search?q=`  | Yazarlarda arama yapar                    |

---

## 🏷️ Türler

| Metod | Endpoint                       | Açıklama                             |
|-------|--------------------------------|--------------------------------------|
| GET   | `/api/Genre`                   | Tüm türleri listeler                 |
| GET   | `/api/Genre/{id}`              | ID'ye göre tür detaylarını getirir   |
| GET   | `/api/Genre/search?name=`      | İsimle tür arar                      |
| GET   | `/api/Genre/exists/{id}`       | Türün varlığını kontrol eder         |

---

## 📝 Blog Yazıları

| Metod | Endpoint                          | Açıklama                                       |
|-------|-----------------------------------|------------------------------------------------|
| GET   | `/api/BlogPost`                   | Tüm blog yazılarını listeler                   |
| GET   | `/api/BlogPost/{id}`              | ID'ye göre yazı detaylarını getirir            |
| POST  | `/api/BlogPost`                   | Yeni blog yazısı oluşturur (Auth)              |
| PUT   | `/api/BlogPost/{id}`              | Yazıyı günceller (Auth, Yazar)                 |
| DELETE| `/api/BlogPost/{id}`              | Yazıyı siler (Auth, Yazar, Admin)              |
| GET   | `/api/BlogPost/user/{userId}`     | Belirli kullanıcının yazılarını getirir        |
| GET   | `/api/BlogPost/my-posts`          | Giriş yapmış kullanıcının yazılarını getirir   |

---

## ⭐ Puanlama ve Yorumlar

| Metod | Endpoint                              | Açıklama                                          |
|-------|---------------------------------------|---------------------------------------------------|
| GET   | `/api/Rating`                         | Tüm puanlamaları listeler                         |
| GET   | `/api/Rating/{id}`                    | Belirli bir puanlama detayını getirir             |
| POST  | `/api/Rating`                         | Yeni puanlama ekler (Auth)                        |
| PUT   | `/api/Rating/{id}`                    | Puanlamayı günceller (Auth, Sahip)                |
| DELETE| `/api/Rating/{id}`                    | Puanlamayı siler (Auth, Sahip, Admin)             |
| GET   | `/api/Rating/book/{bookId}`           | Belirli kitabın puanlamaları                      |
| GET   | `/api/Rating/user/{userId}`           | Belirli kullanıcının puanlamaları                 |
| GET   | `/api/Rating/my-ratings`              | Kendi tüm puanlamaların                           |
| GET   | `/api/Rating/my-rating/book/{bookId}` | Belirli bir kitaba verdiğin puanı getirir         |
| GET   | `/api/Rating/average/{bookId}`        | Kitabın ortalama puanını hesaplar                 |

---

## 📋 Kitap Listeleri

| Metod | Endpoint                                   | Açıklama                                         |
|-------|--------------------------------------------|--------------------------------------------------|
| GET   | `/api/BookList`                            | Tüm listeleri listeler                           |
| GET   | `/api/BookList/{id}`                       | ID'ye göre liste detaylarını getirir             |
| POST  | `/api/BookList`                            | Yeni liste oluşturur (Auth)                      |
| PUT   | `/api/BookList/{id}`                       | Listeyi günceller (Auth, Sahip)                  |
| DELETE| `/api/BookList/{id}`                       | Listeyi siler (Auth, Sahip, Admin)               |
| GET   | `/api/BookList/user/{userId}`              | Kullanıcının listelerini getirir                 |
| GET   | `/api/BookList/my-lists`                   | Giriş yapan kullanıcının listeleri               |
| GET   | `/api/BookList/popular/{count}`            | En çok beğenilen listeleri getirir               |
| POST  | `/api/BookList/{id}/books`                 | Listeye kitap ekler (Auth, Sahip)                |
| DELETE| `/api/BookList/{id}/books/{bookId}`        | Listeden kitap çıkarır (Auth, Sahip)             |

---

## 👍 Beğeni Sistemleri

### 📑 Blog Yazısı Beğenileri

| Metod | Endpoint                              | Açıklama                                          |
|-------|---------------------------------------|---------------------------------------------------|
| GET   | `/api/posts/{postId}/likes/count`     | Yazının toplam beğeni sayısı                      |
| GET   | `/api/posts/{postId}/likes/is-liked`  | Kullanıcı beğenmiş mi? (Auth)                     |
| POST  | `/api/posts/{postId}/likes/toggle`    | Beğeni durumunu değiştir (Auth)                   |

### 📘 Kitap Beğenileri

| Metod | Endpoint                              | Açıklama                                          |
|-------|---------------------------------------|---------------------------------------------------|
| GET   | `/api/books/{bookId}/likes/count`     | Kitabın toplam beğeni sayısı                      |
| GET   | `/api/books/{bookId}/likes/is-liked`  | Kullanıcı beğenmiş mi? (Auth)                     |
| POST  | `/api/books/{bookId}/likes/toggle`    | Beğeni durumunu değiştir (Auth)                   |
| GET   | `/api/users/me/liked-books`           | Kullanıcının beğendiği kitap ID’leri (Auth)       |

### 📋 Liste Beğenileri

| Metod | Endpoint                              | Açıklama                                          |
|-------|---------------------------------------|---------------------------------------------------|
| GET   | `/api/lists/{listId}/likes/count`     | Listenin toplam beğeni sayısı                     |
| GET   | `/api/lists/{listId}/likes/is-liked`  | Kullanıcı beğenmiş mi? (Auth)                     |
| POST  | `/api/lists/{listId}/likes/toggle`    | Beğeni durumunu değiştir (Auth)  
---
## 📂 Ek Bilgiler

- Tüm uç noktalar detaylı şekilde README içerisinde listelenmiştir
- Swagger kullanarak test edebilir veya Postman gibi HTTP client araçlarıyla entegrasyon sağlayabilirsiniz

---


