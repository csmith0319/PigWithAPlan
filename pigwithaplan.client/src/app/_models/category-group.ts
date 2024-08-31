import { ICategory } from 'app/budgets/_models/Category';

export interface ICategoryGroup {
  id: number;
  budgetId?: number;
  name?: string;
  categories?: ICategory[];
}
