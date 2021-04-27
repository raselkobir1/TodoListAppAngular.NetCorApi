export interface UserRegisterVM{
    userName: string;
    email?: string;
    password: string;
    mobile?: string;
    role?:string;
}
 
export interface UserLoginVM{
    userName: string;
    password: string;
    token: string;
}