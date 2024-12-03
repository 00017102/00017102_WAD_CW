import { Component, OnInit } from '@angular/core';
import { CommentModel } from '../../models/comment-model';
import { ActivatedRoute } from '@angular/router';
import { Post } from '../../models/post';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-post-detail',
  standalone: false,
  
  templateUrl: './post-detail.component.html',
  styleUrl: './post-detail.component.css'
})
export class PostDetailComponent implements OnInit {
  post: Post | null = null;
  comments: CommentModel[] = [];
  newComment: string = '';

  constructor(
    private route: ActivatedRoute,
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
    const postId = +this.route.snapshot.paramMap.get('id')!;
    this.apiService.getPostById(postId).subscribe((data) => {
      this.post = data;
      this.comments = data.comments || [];
    });
  }

  addComment(): void {
    if (this.newComment.trim()) {
      const comment: CommentModel = {
        postId: this.post?.id!,
        content: this.newComment,
        authorName: 'Anonymous',
      };
      this.apiService.createComment(comment).subscribe((data) => {
        if (data != null) {
          this.comments.push(data);
          this.newComment = '';
        }
      });
    }
  }
}
