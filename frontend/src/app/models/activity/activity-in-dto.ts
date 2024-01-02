export class ActivityInDTO {
  constructor(
    public title: string,
    public description: string,
    public dateTime: string,
    public imageUrl: string | null
  ) {}
}
