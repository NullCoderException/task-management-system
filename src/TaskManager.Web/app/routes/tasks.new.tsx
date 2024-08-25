import { ActionFunction, redirect } from "@remix-run/node";
import { Form } from "@remix-run/react";
import TaskForm from "~/components/TaskForm";
import { Task } from "~/types/task";

// Mock function to create a task
const createTask = async (task: Omit<Task, "id">): Promise<Task> => {
  return { ...task, id: Date.now().toString() };
};

export const action: ActionFunction = async ({ request }) => {
  const formData = await request.formData();
  const title = formData.get("title") as string;
  const description = formData.get("description") as string;
  const status = formData.get("status") as Task["status"];
  const dueDate = formData.get("dueDate")
    ? new Date(formData.get("dueDate") as string)
    : undefined;

  const newTask = await createTask({ title, description, status, dueDate });

  return redirect(`/tasks/${newTask.id}`);
};

export default function NewTask() {
  return (
    <div className="max-w-2xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Create New Task</h1>
      <Form method="post">
        <TaskForm onSubmit={() => {}} />
      </Form>
    </div>
  );
}
