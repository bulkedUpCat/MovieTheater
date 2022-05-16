export interface User{
  id: string;
  email: string;
  userName: string;
  comments: Array<Comment>;
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

export class ChangeUsernameDTO{
  userId: string;
  newUsername: string;
}

export interface ChangePasswordDTO{
  userId: string;
  newPassword: string;
}

export class ChangeEmailDTO{
  userId: string;
  newEmail: string;
}

export interface ForgotPasswordDTO{
  email: string;
  clientURI: string;
}

export interface ResetPasswordDTO{
  password: string;
  confirmPassword: string;
  email: string;
  token: string;
}
