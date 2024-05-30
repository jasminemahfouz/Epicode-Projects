import { Component, ViewChild } from '@angular/core';
import { PostInterface } from 'src/interfaces/post.interface';
import { SinglePostComponent } from '../single-post/single-post.component';
import { PostsService } from 'src/services/posts.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss'],
})
export class HomepageComponent {
  post!: PostInterface;
  related: PostInterface[] = [];
  posts!:PostInterface[]
  tags!: string[]
  filter!: string
  @ViewChild('singlePost') singlePost!: SinglePostComponent;

  constructor(private postSrv: PostsService) {
    this.post = this.postSrv.topPost;
    this.posts = this.postSrv.posts
    this.related = this.postSrv.getRandomPosts(4);
    this.tags = this.postSrv.tags
  }

  editPost() {
    alert('Edit successful!');
  }
  filterPosts(tag:string) {
    this.filter = tag
    this.posts = this.postSrv.getFilteredPosts(this.filter)
  }
}
