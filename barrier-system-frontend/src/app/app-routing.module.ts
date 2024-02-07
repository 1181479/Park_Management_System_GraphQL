import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { BarrierComponent } from './barrier/barrier.component';

export const routes: Routes = [
  { path: '', redirectTo: '/barrier', pathMatch: 'full' },
  { path: 'barrier', component: BarrierComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
