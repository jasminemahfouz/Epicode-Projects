import { Component } from '@angular/core';
import { PostInterface } from 'src/interfaces/post.interface';
import { PostsService } from 'src/services/posts.service';

@Component({
  selector: 'app-inactive-posts',
  templateUrl: './inactive-posts.component.html',
  styleUrls: ['./inactive-posts.component.scss'],
})
export class InactivePostsComponent {
  inactive!: PostInterface[];
  constructor(private postSrv: PostsService) {
    this.inactive = this.postSrv.inactivePosts;
  }
}
