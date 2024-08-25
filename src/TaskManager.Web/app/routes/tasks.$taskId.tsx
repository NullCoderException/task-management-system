import { json, LoaderFunction, SerializeFrom } from "@remix-run/node";
import { useLoaderData } from "@remix-run/react";
import TaskDetail from "~/components/TaskDetail";
import { Task } from "~/types/task";

// Mock function to fetch a single task
const getTask = async (id: string): Promise<Task> => {
  return {
    id,
    title: `Task ${id}`,
    description: `Description ${id}`,
    status: "todo",
  };
};

export const loader: LoaderFunction = async ({ params }) => {
  const taskId = params.taskId;
  if (!taskId) throw new Error("Task ID is required");

  const task = await getTask(taskId);
  return json({ task });
};

export default function TaskDetailPage() {
  const { task } = useLoaderData<SerializeFrom<typeof loader>>();

  return (
    <div className="max-w-2xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Task Details</h1>
      <TaskDetail task={task} />
    </div>
  );
}
