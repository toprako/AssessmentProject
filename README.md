# AssessmentProject
Bu Bir Örnek Projedir.
Bir Rehber Uygulaması Bu Uygulamada Rehbere Kişi Ekleme, Rehberden Kişi Silme, Rehberdeki Kişiye Bağlı İletişim Bilgisini Ekleme, Rehberdeki Kişiye Bağlı İletişim Bilgisini Kaldırma ve buna bağlı bir rapor oluşturmaktadır. Raporda Konum Bilgisi , O konumda yer alan rehbere kayıtlı kişi sayısı , O konumda yer alan rehbere kayıtlı telefon numarası sayısı elde edilmektedir.

# Ön Koşullar
Projeyi Çalıştırmadan Önce RabbitMq servisi çalıştırmak ve çalışır halde olmak zorundadır. Database Olarak Postgresql kullanılmış ve adres bilgilerini appconfig.json datasından değiştirerek bağlantı sağlanabilir. Veritabanı Migration işlemleri için Person Projesini "Set As Startup Project" Olarak Seçiniz ve Package Manager Console Üzerinden "update-database" yazılarak veritabanı aktarımı sağlanmaktadır.

# Request İşlemleri
# Kayıt İşlemi 
Url Adresi => {url}/Person/AddPerson
Örnek Request => 
{
    "Name": "Hüseyin",
    "SurName": "Yılmaz",
    "InformationTypes": [
        {
            "InformationType": 2,
            "InformationContent": "Kocaeli"
        },
        {
            "InformationType": 1,
            "InformationContent": "119-45-45"
        },
        {
            "InformationType": 2,
            "InformationContent": "İstanbul"
        }
    ]
}

# Silme İşlemi 
Url Adresi => {url}/Person/DeletePerson
Örnek Request => 
{
    "Id": "95ce5570-5f44-428d-a831-6b77c95046ad"
}

# İlgili Kişiye Ait İletişim Bilgisi Ekleme İşlemi 
Url Adresi => {url}/Person/AddPersonCommunication
Örnek Request => 
{
    "Id": "43f85c5e-ed77-49e7-8bd6-8b09ae9f9f08",
    "InformationType": 2,
    "InformationContent": "Harita Test"
}

# İlgili Kişiye Ait İletişim Bilgisini Silme İşlemi
Url Adresi => {url}//Person/DeletePersonCommunication
Örnek Request => 
{
    "Id": "43f85c5e-ed77-49e7-8bd6-8b09ae9f9f08",
    "CommunicationId": "2d6036a4-9770-4ed8-befd-f6a2fbb76bb9"
}

# Rehberdeki Tüm Kişileri Çekme
Url Adresi => {url}/Person/GetAll
Örnek Request => Get Method

# Rehberdeki İlgili Idye Göre Kişiyi Çekme
Url Adresi => {url}/Person/GetByPerson?Id=43f85c5e-ed77-49e7-8bd6-8b09ae9f9f08
Örnek Request => Get Method

# Rehberdeki Datalara Göre Rapor Oluşturuması 
Url Adresi => {url}/Person/GetPersonByLocationReport
