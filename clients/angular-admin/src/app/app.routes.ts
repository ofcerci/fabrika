import { Routes } from "@angular/router";
import { LoginPageComponent } from "./pages/login-page.component";
import { DashboardPageComponent } from "./pages/dashboard-page.component";
import { ResourcePageComponent } from "./pages/resource-page.component";
import { ShowcasePageComponent } from "./pages/showcase-page.component";
import { authGuard } from "./core/auth.guard";

export const routes: Routes = [
  { path: "login", component: LoginPageComponent },
  { path: "dashboard", component: DashboardPageComponent, canActivate: [authGuard] },
  { path: "user", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "user" } },
  { path: "group", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "group" } },
  { path: "language", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "language" } },
  { path: "translate", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "translate" } },
  { path: "operationclaim", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "operationclaim" } },
  { path: "log", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "log" } },
  { path: "showcase", component: ShowcasePageComponent, canActivate: [authGuard] },

  // Gym Modülleri
  { path: "member", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "member" } },
  { path: "package", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "package" } },
  { path: "trainer", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "trainer" } },
  { path: "subscription", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "subscription" } },
  { path: "lesson", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "lesson" } },
  { path: "reservation", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "reservation" } },
  { path: "attendance", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "attendance" } },
  { path: "payment", component: ResourcePageComponent, canActivate: [authGuard], data: { moduleKey: "payment" } },

  { path: "", pathMatch: "full", redirectTo: "dashboard" },
  { path: "**", redirectTo: "dashboard" }
];
