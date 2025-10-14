export type User = {
    id: string;
    displayName: string;
    email: string;
    token: string;
    imageUrl?: string;
}

//Nos permite modelar un objeto con cierto tipo de propiedades
//Los dos nos sirven, el de arriba y este para gaurdar datos. 


export type LoginCreds = {
    email: string;
    password: string;
}

export type RegisterCreds = {
    email: string;
    displayName: string;
    password: string;
}