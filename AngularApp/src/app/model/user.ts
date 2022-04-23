export interface User{
  id: string,
  email: string
}

export interface UserLogin {
  login: string;
  email: string;
  password: string;
}

export interface UserSignUp{
  name: string;
  login: string;
  email: string;
  password: string;
  confirmPassword: string;
  age: number;
}
