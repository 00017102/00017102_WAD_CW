import { CommentModel } from "./comment-model";


export class Post {
  id?: number; // Optional for creation
  title!: string;
  content!: string;
  authorName!: string;
  createdDate?: Date;
  lastModifiedDate?: Date;
  categoryId!: number;
  categoryName?: string; // Optional for response
  comments?: CommentModel[];  // Optional for response
}
