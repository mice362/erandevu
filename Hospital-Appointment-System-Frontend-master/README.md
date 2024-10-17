# Hastane Randevu Sistemi📝
Bu proje Tobeto ile gerçekleştirilen .NET & Angular Full Stack eğitiminin bitirme projesinin frontend tarafıdır. 

<p>📌Projenin backend kısmına <a href=https://github.com/mervekaratass/Hospital>buradan</a>  ulaşabilirsiniz.</p>

#### GEREKSİNİMLER 🛠
- [x] Web projesi: 
  ![Asp.NET Web API](https://img.shields.io/badge/asp.net%20web%20api-%231BA3E8.svg?style=for-the-badge&logo=dotnet&logoColor=white)
  ![Angular](https://img.shields.io/badge/angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white)
- [x] Veri tabanı: 
  ![MsSQL Server](https://img.shields.io/badge/mssql%20server-%23CC2927.svg?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)


#### PROJEDE KULLANILAN PROGRAMLAMA DİLLERİ VE TEKNOLOJİLER 🎯
<p>
  <img alt="HTML5" src="https://img.shields.io/badge/-HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white" />
  <img alt="CSS" src="https://img.shields.io/badge/CSS-239120?style=for-the-badge&logo=css3&logoColor=white" />
  <img alt="JavaScript" src="https://img.shields.io/badge/JavaScript-%23323330.svg?style=for-the-badge&logo=javascript&logoColor=white" />
  <img alt="TypeScript" src="https://img.shields.io/badge/TypeScript-%23007ACC.svg?style=for-the-badge&logo=typescript&logoColor=white" />
  <img alt="Bootstrap" src="https://img.shields.io/badge/-Bootstrap-%238511FA.svg?style=for-the-badge&logo=bootstrap&logoColor=white" />
  <img alt="Angular" src="https://img.shields.io/badge/-Angular-%23DD0031.svg?style=for-the-badge&logo=angular&logoColor=white" />
  <img alt="Toastr" src="https://img.shields.io/badge/Toastr-%23FF0000.svg?style=for-the-badge&logo=generic&logoColor=white" />
  <img alt="Visual Studio Code" src="https://img.shields.io/badge/-Visual%20Studio%20Code-0078d7.svg?style=for-the-badge&logo=visual-studio-code&logoColor=white" />
  <img alt="GitHub" src="https://img.shields.io/badge/GitHub-%23121011.svg?style=for-the-badge&logo=github&logoColor=white" />
</p>

#### 🎯 NASIL BİR PROJE OLUŞTURDUK?
<p>Bu proje, hastaların randevu almasını, geçmiş ve gelecek randevularını takip etmelerini ve doktorlar ile kolayca iletişim kurmalarını sağlayan, kullanıcı dostu bir hastane randevu sistemidir.</p>

<p> Üç tip kullanıcı bulunmaktadır: </p>

➡️ 1- Admin/Yönetici 

- [x] Hasta bilgilerini listeler,düzenler ve siler. Gerektiğinde yeni hasta ekleyebilir.
- [X] Hastaneye doktor ataması yapar. Doktor bilgilerini listeler, günceller ve siler. 
- [x] Mevcut branşları listeler,düzenler ve siler. Hastaneye branş eklemesi yapar.
- [x] Geçmiş ve gelecek tüm randevu detaylarını listeler. Yeni randevu oluşturabilir.
- [x] Yazılmış raporlar detaylarını (rapor içeriği hariç) görüntüleyebilir.
- [x] Kullanıcılar tarafından oluşturulan tüm öneri & şikayet geri bildirimlerini listeler. Geri bildirimi onaylama ve silme hakkına sahiptir.
- [x] Toplam randevu sayısı, toplam doktor sayısı ve toplam branş sayısı gibi metrikleri gösteren İstatistikleri görüntüleyebilir.
- [x] Kendi bilgilerini güncelleyebilir ve şifre değişikliği yapabilir.

➡️ 2- Doktor 
- [x] Bugünkü Randevularım - Yarınki Randevularım - Çalışma Takvimim - Hasta Raporları alanlarını içeren Özet sayfasını görüntüleyebilir.
- [x] Kendi çalışma takvimini oluşturabilir. Gerektiğinde çalışma takvimini güncelleyebilir veya silebilir.
- [x] Randevulu hastalarının bilgilerini görüntüleyebilir. 
- [x] Hastaların kendisinden aldığı randevuları Geçmiş Randevular & Gelecek Randevular alanında görüntüleyebilir.
- [x] Geçmiş randevular için rapor oluşturabilir.
- [x] Oluşturduğu raporların detaylarını görüntüleyebilir.
- [x] Öneri & Şikayet için oluşturduğu geri bildirimleri görüntüleyebilir ve silebilir. 
- [x] Kendi bilgilerini güncelleyebilir.

➡️ 3- Hasta 
- [x] Bugünkü Randevularım - Gelecek Randevularım - Raporlarım - Geri Bildirimlerim alanlarını içeren Özet sayfasını görüntüleyebilir.
- [x] İstediği branş ve doktora randevu alabilir. Gerektiğinde iptal edebilir.
- [x] Aldığı randevuları Geçmiş Randevular & Gelecek Randevular alanında görüntüleyebilir.
- [x] Doktorun oluşturduğu raporların detaylarını görüntüleyebilir.
- [x] Öneri & Şikayet için oluşturduğu geri bildirimleri görüntüleyebilir ve silebilir. 
- [x] Kendi bilgilerini güncelleyebilir.


## PROJE DETAYLARI📝

Projede Angular, modern ve kullanıcı dostu bir kullanıcı arayüzü sağlamak için kullanılmaktadır. Angular, güçlü bir framework olarak, SPA (Single Page Application) yapısını destekleyerek kullanıcı deneyimini optimize etmeye olanak tanır.

Angular kullanırken projede şu özellikler ve kütüphaneler önemli rol oynamaktadır:

- **Reactive Forms ve Template-Driven Forms**: Form işlemlerini yönetmek için kullanılan Angular bileşenleri.

- **HttpClient**: Backend API'ler ile iletişim kurmak için kullanılan Angular modülü.

- **Routing**: Uygulama içi navigasyonu ve sayfa yönetimini sağlar.

- **RxJS** (Reactive Extensions for JavaScript): Asenkron veri akışını yönetmek ve işlemek için kullanılan kütüphane.

- **Angular Material**: Material Design prensiplerine uygun olarak hazırlanmış UI bileşenlerini kolayca entegre etmek için kullanılır.

Bu bileşenler ve kütüphaneler sayesinde Angular, projenizin frontend tarafında güçlü bir performans ve kullanılabilirlik sunmuştur.


<p>📃Kullanıcılar siteye girdiklerinde onları karşılayan bir anasayfamız bulunmaktadır. Bu sayfada hastanemizle ilgili bilgilendirme yazılarının yanı sıra, oluşturduğumuz Footer ve Header yapıları sayesinde
kullanıcıların ihtiyaç duydukları bilgilere kolayca ulaşmaları sağlanmaktadır. Bunların yanı sıra kullanıcılar anasayfadan ulaşabilecekleri tıbbi birimler, doktorlarımız, hakkımızda, bize ulaşın gibi diğer sayfalara da yönlendirilirler.</br></p>




<img src="https://github.com/user-attachments/assets/f2a5e4f5-5c13-47dc-8e58-dc200783d888" alt="image" width="700">
<img src="https://github.com/user-attachments/assets/a10b2858-39a8-4b0a-a7ba-9eb268bc203a" alt="image" width="700">

<p></br>➡️Ek olarak Sıkça Sorulan Sorular ve  Canlı Destek sayesinde kullanıcıların sorularına çok daha kolay ve hızlı cevap alabilmeleri sağlanır. </br></p>

<img width="700" alt="image" src="https://github.com/user-attachments/assets/42ce9b87-bb7c-4847-bf36-0e507fffcde4">


<p></br>✎ Hastane randevu sisteminde, hastaların randevu alabilmesi için sisteme giriş yapmaları zorunlu kılınmıştır. Bu süreç, hem güvenlik hem de kişisel sağlık bilgilerinin korunması açısından kritik öneme sahiptir. Yeni hastaların sisteme üye olabilmesi için kullanıcı dostu bir Üye Ol sayfası oluşturulmuştur. Bu sayfa, hastaların gerekli bilgilerini kolayca girebilecekleri bir form içerir. Üyelik sürecinde hastalardan ad, soyad, e-posta adresi ve telefon numarası gibi temel bilgilerin yanı sıra, güvenli bir şifre oluşturmaları istenmektedir. Üye olduktan sonra hastanın sisteme giriş yapabilmesi için mail adresini doğrulama şartı eklenmiştir. Bu sayede hastanın üye olurken girmiş olduğu mail adresine bir doğrulama linki yollanır. Hasta bu link aracılığıyla e-posta adresini doğrularsa sisteme giriş yapabilmektedir, aksi taktirde sisteme giriş yapabilmesi mümkün olmayacaktır. Şifresini unutan hastalar ise e-posta veya telefon numaraları aracılığıyla yeni bir şifre talep edebilir ve şifrelerini güncelleyebilirler. Bu sayede, hastalarımızın sisteme erişimlerinin kesintisiz ve güvenli bir şekilde sağlanması hedeflenmektedir.</br></p>

<img width="700" alt="image" src="https://github.com/user-attachments/assets/84e9f83b-ae36-4019-a95c-12070316b4f9">

<p>📌Kullanıcılar sistme giriş yaptıktan sonra yetkinliklerine göre ilgili panellere yönlendirilirler. Bu panellerden kendilerine sunulan özelliklerden istediklerine erişip kullanabilirler. Projeyi daha detaylı incelemek ve diğer sayfaları görmek isterseniz indirebilirisiniz ya da <a href="https://www.canva.com/design/DAGJ_tDryyw/pvyKpd8p9P1Oypok3TDjVQ/edit?utm_content=DAGJ_tDryyw&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton">Canva</a> 'da bulunan sunumdan diğer ekranları görebilirsiniz.</p>

<img width="700" alt="image" src="https://github.com/user-attachments/assets/d7cc74e3-bfee-4829-9314-054d0d315b16">



-----------------------------------------------------------------------

Görüşürüz 🎉

## Badges

Add badges from somewhere like: [shields.io](https://shields.io/)

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/)
[![AGPL License](https://img.shields.io/badge/license-AGPL-blue.svg)](http://www.gnu.org/licenses/agpl-3.0)
