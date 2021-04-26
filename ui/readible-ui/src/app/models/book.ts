export class Book {
  constructor(
    public id: number,
    public name: string,
    public author: string,
    public description: string,
    public rating: number,
    public imageUrl: string,
  ) { }
}
