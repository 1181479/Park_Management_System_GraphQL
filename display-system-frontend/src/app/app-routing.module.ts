import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { MessageComponent } from './message/message.component';

export const routes: Routes = [
  { path: '', redirectTo: '/message', pathMatch: 'full' },
  { path: 'message', component: MessageComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
