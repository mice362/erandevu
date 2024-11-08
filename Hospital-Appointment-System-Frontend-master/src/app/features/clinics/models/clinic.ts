export interface Clinic {
    id: number;
    name: string;
    phone: string;
    address: string;
    email: string;
    about: string;
    logoName: string;
    logo: Uint8Array; // C#'daki byte[] tipine karşılık olarak Uint8Array kullanılır
}
