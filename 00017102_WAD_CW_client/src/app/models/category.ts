import { Post } from "./post";

export class Category {
  id?: number; // Optional for creation
  name!: string;
  posts?: Post[]; // Optional for response
}
