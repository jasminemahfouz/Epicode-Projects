import { Component } from '@angular/core';
import { PostInterface } from 'src/interfaces/post.interface';

@Component({
  selector: 'app-inactive-posts',
  templateUrl: './inactive-posts.component.html',
  styleUrls: ['./inactive-posts.component.scss']
})
export class InactivePostsComponent {
  posts!: PostInterface[];
  constructor() {
    this.getInactivePosts().then((res) => (this.posts = res));
  }
  async getInactivePosts(): Promise<PostInterface[]> {
    const res = await fetch('../../assets/db.json');
    const posts = (await res.json()) as Array<PostInterface>;

    return posts.filter((post) => !post.active);
  }
}
