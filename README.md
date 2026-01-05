# JobPortal API

JobPortal API, .NET 7 ile geliştirilmiş; JWT tabanlı kimlik doğrulama,
rol bazlı yetkilendirme ve modern backend mimari yaklaşımlarını içeren
bir Web API projesidir.

Gerçek hayatta karşılaşılan
ihtiyaçlar (authentication, pagination, filtering, hata yönetimi vb.)
dikkate alınmıştır.

---

## Kullanılan Teknolojiler

- .NET 7 Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication & Authorization
- AutoMapper
- Dependency Injection
- Global Exception Handling
- DTO & Service Layer mimarisi

---

## Genel Mimari Yaklaşım

Proje katmanlı bir yapı ile geliştirilmiştir:

Controller → Service → Data (DbContext)

- Controller'lar sadece HTTP isteklerini karşılar
- İş kuralları Service katmanında yer alır
- DTO ↔ Entity dönüşümleri AutoMapper ile yapılır
- Hata yönetimi global middleware üzerinden sağlanır

Amaç; okunabilir, test edilebilir ve sürdürülebilir bir backend mimarisi
oluşturmaktır.

---

## Authentication & Authorization

- JWT tabanlı kimlik doğrulama
- Role-based authorization (Admin / User)
- Token içerisinde Email ve Role bilgisi yer alır

Korumalı endpoint'ler için: Authorization: Bearer {JWT_TOKEN}

---

## Global API Response Yapısı

Tüm endpoint'ler standart bir response formatı döner:

{ "success": true, "message": "İşlem başarılı", "data": {} }

Bu yapı sayesinde frontend tarafında response yönetimi kolaylaşır.

---

## Job Modülü

- CRUD işlemleri
- Admin yetkili create
- Filtering, Sorting
- Pagination

Örnek: GET /api/jobs?page=1&pageSize=10&sortBy=salary&desc=true

---

## AutoMapper

DTO ↔ Entity dönüşümleri AutoMapper ile merkezi şekilde yönetilmiştir.
Service kodları sadeleştirilmiştir.

---

## Global Exception Handling

Controller'larda try/catch kullanılmıyor.
Tüm hatalar ExceptionMiddleware tarafından yakalanıyor.
