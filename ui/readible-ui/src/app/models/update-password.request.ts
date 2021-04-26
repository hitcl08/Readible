export class UpdatePasswordRequest {
  constructor(
    public userId: number,
    public password: string
  ){}
}
