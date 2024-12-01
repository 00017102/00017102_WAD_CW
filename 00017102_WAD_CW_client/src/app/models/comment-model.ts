export class CommentModel {
  id?: number; // Optional for creation
  content!: string;
  authorName!: string;
  createdDate?: Date;
  postId!: number;
}
