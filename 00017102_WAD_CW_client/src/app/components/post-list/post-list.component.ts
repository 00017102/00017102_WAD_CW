import { Component, OnInit } from '@angular/core';
import { Post } from '../../models/post';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-post-list',
  
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit{
  posts: Post[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getPosts().subscribe((data) => {
      this.posts = data;
    });
  }
}
