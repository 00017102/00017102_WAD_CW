import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from '../../models/post';
import { ApiService } from '../../services/api.service';
import { CommentModel } from '../../models/comment-model';
import { Category } from '../../models/category';
import { MatDialog } from '@angular/material/dialog';
import { AddCategoryDialogComponent } from '../add-category-dialog/add-category-dialog.component';

@Component({
  selector: 'app-post-details',
  standalone: false,
  
  templateUrl: './post-details.component.html',
  styleUrl: './post-details.component.css'
})
export class PostDetailsComponent implements OnInit {
  post: Post | null = null;
  comments: CommentModel[] = [];
  categories: Category[] = [];
  newComment: string = '';
  editMode: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService,
    private dialog: MatDialog,
  ) { }

  ngOnInit(): void {
    const postId = +this.route.snapshot.paramMap.get('id')!;
    this.apiService.getPostById(postId).subscribe((data) => {
      this.post = data;
      this.comments = data.comments || [];
    });
    this.loadCategories();
  }

  loadCategories(): void {
    this.apiService.getCategories().subscribe((data) => {
      this.categories = data;
    });
  }

  addComment(): void {
    if(this.newComment.trim()) {
      const comment: CommentModel = {
      content: this.newComment,
      authorName: 'Anonymous', // Replace with dynamic user input if needed
      postId: this.post?.id!,
    };
      this.apiService.createComment(comment).subscribe(() => {
        this.comments.push(comment);
        this.newComment = '';
      });
    }
  }

  toggleEditMode(): void {
    this.editMode = !this.editMode;
  }

  savePost(): void {
    if (this.post) {
      this.apiService.updatePost(this.post.id!, this.post).subscribe(() => {
        this.editMode = false;
      });
    }
  }

  openCategoryDialog(): void {
    const dialogRef = this.dialog.open(AddCategoryDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadCategories(); // Reload categories after addition
      }
    });
  }

}
