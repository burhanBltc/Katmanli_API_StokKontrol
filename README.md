🛒 Stok Kontrol Projesi
📌 Proje, 4 katmandan oluşan bir mimariye sahiptir:
1️⃣ UI (Kullanıcı Arayüzü - ASP.NET Core MVC)
2️⃣ API (Veri Sağlayıcı Katman - ASP.NET Core Web API)
3️⃣ Repositories (Veri Erişim Katmanı - Entity Framework Core)
4️⃣ Entities (Model ve Enum Tanımları)
-----------------------------------------
🖥️ UI (ASP.NET Core MVC)
Kullanıcı arayüzü, ASP.NET Core MVC kullanılarak geliştirilmiştir.
HttpClient ile API endpointlerine istek atarak veri alışverişi sağlanmaktadır. Kullanıcı doğrulama işlemleri de API üzerinden gerçekleştirilir. Başarılı giriş yapıldığında, kullanıcı bilgileri Claims olarak saklanır ve ASP.NET Core'un Cookie Authentication sistemi ile oturum yönetilir.
Category, Product, Supplier gibi CRUD işlemleri için ilgili Controller'lar, API ile entegre çalışmaktadır.
------------------------------------------
🌐 API (ASP.NET Core Web API)
UI katmanından gelen istekleri işleyerek, veritabanından gerekli verileri alır veya günceller.
Kullanıcı giriş işlemleri ve doğrulama işlemleri burada gerçekleştirilir.
Kategori, Ürün, Tedarikçi gibi modeller için gerekli API endpoint'leri burada yazılmıştır.
-------------------------------------------
📂 Repositories (Veri Erişim Katmanı - Repository Pattern & EF Core)
Veri tabanı bağlantıları ve Generic Repository Pattern burada yazılmıştır.
Tüm veri tabanı işlemleri (CRUD, Transaction işlemleri) burada yönetilir.
Entity Framework Core kullanılarak veritabanı işlemleri gerçekleştirilir.
-------------------------------------------
📄 Entities (Model Katmanı)
Veritabanı modelleri (Category, Product, Supplier, User, vb.) burada tanımlanmıştır.
Enum’lar ve temel Entity sınıfları burada tutulur.
-------------------------------------------
🚀 Teknolojiler
✅ ASP.NET Core MVC (UI)
✅ ASP.NET Core Web API (API)
✅ Entity Framework Core (Veri Erişim)
✅ Generic Repository Pattern
✅ Cookie Authentication & Claims-Based Authentication
✅ HttpClient ile API Tüketimi
✅ View'larda HTML, CSS

