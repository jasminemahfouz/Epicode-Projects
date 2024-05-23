import { Component, OnInit } from '@angular/core';
import { PostInterface } from 'src/interfaces/post.interface';

@Component({
  selector: 'app-active-posts',
  templateUrl: './active-posts.component.html',
  styleUrls: ['./active-posts.component.scss'],
})
export class ActivePostsComponent {
  posts!: PostInterface[];
  constructor() {
    this.getActivePosts().then((res) => (this.posts = res));
  }
  async getActivePosts(): Promise<PostInterface[]> {
    const res = await fetch('../../assets/db.json');
    const posts = (await res.json()) as Array<PostInterface>;

    return posts.filter((post) => post.active);
  }
}
